using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Collections.ObjectModel;

namespace EntityFrameworkExample.Entitites
{
    public class MovieTheater: Notification
    {
        public int Id { get; set; }
        private string _name;
        public string Name { get => _name; set => Set(value, ref _name); }
        private Point _location;
        public Point Location { get => _location; set => Set(value, ref _location); }
        private MovieTheaterOffer _movieTheaterOffer;
        public MovieTheaterOffer MovieTheaterOffer { get => _movieTheaterOffer; set => Set(value, ref _movieTheaterOffer); }
        public ObservableCollection<Cinema> Cinemas { get; set; }
        private MovieTheaterDetails _cinemas;
        public MovieTheaterDetails MovieTheaterDetails { get => _cinemas; set => Set(value, ref _cinemas); }
        private Address _address;
        public Address Address { get => _address; set => Set(value, ref _address); }
    }
}
