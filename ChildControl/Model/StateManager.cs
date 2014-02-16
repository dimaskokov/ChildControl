using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Info;
using System.Device.Location;

namespace ChildControl.Model
{
    public class StateManager
    {

        private static async Task<Geocoordinate> GetLocation()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                return geoposition.Coordinate;
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                }
                //else
                {
                    // something else happened acquring the location
                }
            }
            return null;
        }

        public static byte[] DeviceId()
        {
            object uniqueId;
            if (DeviceExtendedProperties.TryGetValue(
            "DeviceUniqueId", out uniqueId))
            {
                return (byte[])uniqueId;
            }
            return null;
        }

        private byte[] _deviceGuid;

        private List<PosState> _stateList;

        private Windows.Storage.ApplicationDataContainer _roamingSettings;

        public StateManager(List<PosState> stateList)
        {
            _deviceGuid = DeviceId();

            _stateList = stateList;
        }

        public async Task<PosState> CalcState()
        {
            var location = await GetLocation();

            var currLocation = new GeoCoordinate(location.Latitude, location.Longitude);
            var currPosState = _stateList.FirstOrDefault(x => currLocation.GetDistanceTo(x.Coord) <= x.Distance);

            return currPosState;
        }

        public void RemoveState(PosState state)
        {
            _stateList.Remove(state);
            _roamingSettings.Values["stateList"] = _stateList;
        }

        public async Task<PosState> AddState(string state, double distance)
        {
            var location = await GetLocation();
            var currLocation = new GeoCoordinate(location.Latitude, location.Longitude);

            var posState = new PosState {Coord = currLocation, Distance = distance, State = state};

            _stateList.Add(posState);
            _roamingSettings.Values["stateList"] = _stateList;

            return posState;
        }

    }

    public class PosState
    {
        public GeoCoordinate Coord { get; set; }
        public double Distance { get; set; }
        public string State { get; set; }
    }
}
