using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TownUtilityBillSystem.Models
{
    public class AddressModel
    {
        public Index Index;
        public Town Town;
        public Street Street;
        public Building Building;
        public FlatPart FlatPart;
        public List<Index> Indexes;
        public List<Town> Towns;
        public List<Street> Streets;
        public List<Building> Buildings;
        public List<FlatPart> FlatParts;

        public AddressModel()
        {
            Index = new Index();
            Town = new Town();
            Street = new Street();
            Building = new Building();
            FlatPart = new FlatPart();

            Indexes = new List<Index>();
            Towns = new List<Town>();
            Streets = new List<Street>();
            Buildings = new List<Building>();
            FlatParts = new List<FlatPart>();
        }

    }
}