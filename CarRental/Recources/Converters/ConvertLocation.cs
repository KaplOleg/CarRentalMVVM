using CarRental.Recources.Converters;
using MapControl;
using System;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CarRental.Recources.Converter
{
    [MarkupExtensionReturnType(typeof(ConvertLocation))]
    [ValueConversion(typeof(DbGeography), typeof(Location))]
    public class ConvertLocation : ConverterBase
    {
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            {
                if (!(v is DbGeography point)) return null;
                return new Location(point.Latitude.Value, point.Longitude.Value);
            }
        }
        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            if (!(v is Location location)) return null;
            return DbGeography.FromText(String.Format(CultureInfo.InvariantCulture, "POINT({0} {1})", location.Longitude, location.Latitude));
        }
    }
}
