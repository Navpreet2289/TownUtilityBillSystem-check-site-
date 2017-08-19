using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TownUtilityBillSystem.Models;

namespace TownUtilityBillSystem.Controllers
{
    public class InitialDBController : Controller
    {
        static Random rnd = new Random();
        static public int RndMin = 500000;
        static public int RndMax = 800000;
        public int Account = rnd.Next(RndMin, RndMax);

        public enum Utilities
        {
            Electricity = 1,
            Water,
            Heating,
            Gas
        }
        enum Surnames
        {
            Jensen,
            Nielsen,
            Hansen,
            Pedersen,
            Andersen,
            Christensen,
            Larsen,
            Soerensen,
            Rasmussen,
            Joergensen,
            Petersen,
            Madsen,
            Kristensen,
            Olsen,
            Thomsen,
            Christiansen,
            Poulsen,
            Johansen,
            Moeller,
            Mortensen,
            Knudsen,
            Jakobsen,
            Jacobsen,
            Mikkelsen,
            Decksen,
            Frederiksen,
            Laursen,
            Henriksen,
            Lund,
            Schmidt,
            Holm,
            Eriksen,
            Kristiansen,
            Simonsen,
            Clausen,
            Svendsen,
            Andreasen,
            Iversen,
            Jeppesen,
            Vestergaard,
            Nissen,
            Lauridsen,
            Jespersen,
            Mogensen,
            Noergaard,
            Jepsen,
            Frandsen,
            Jessen,
            Dahl,
            Skov,
            Christoffersen,
            Bertelsen,
            Bruun,
            Lassen,
            Gregersen,
            Bach,
            Friis,
            Kjeldsen,
            Johnsen,
            Steffensen,
            Lauritsen,
            Krogh,
            Bech,
            Danielsen,
            Andresen,
            Mathiesen,
            Toft,
            Winther,
            Ravn,
            Brandt,
            Dam,
            Holst,
            Lind,
            Mathiasen,
            Berg,
            Overgaard,
            Bak,
            Nilsson,
            Schultz,
            Klausen,
            Kristoffersen,
            Paulsen,
            Schou,
            Koch,
            Thorsen,
            Hermansen,
            Thygesen,
            Nygaard,
            Bang,
            Kruse
        }
        enum Names
        {
            William,
            Noah,
            Lucas,
            Emil,
            Oliver,
            Oscar,
            Victor,
            Malthe,
            Alfred,
            Carl,
            Frederik,
            Elias,
            Magnus,
            Valdemar,
            Villads,
            Alexander,
            Christian,
            August,
            Johan,
            Felix,
            Anton,
            Nohr,
            Liam,
            Aksel,
            Mikkel,
            Benjamin,
            Mads,
            Storm,
            Mathias,
            Adam,
            Sebastian,
            Albert,
            Theodor,
            Arthur,
            Sofia,
            Ida,
            Freja,
            Clara,
            Laura,
            Anna,
            Ella,
            Isabella,
            Karla,
            Alma,
            Josefine,
            Olivia,
            Alberte,
            Maja,
            Sofie,
            Mathilde,
            Agnes,
            Lærke,
            Caroline,
            Liva,
            Emily,
            Sara,
            Victoria,
            Emilie,
            Mille,
            Frida,
            Marie,
            Ellen,
            Rosa,
            Lea,
            Signe,
            Filippa,
            Julie,
            Nora,
            Liv,
            Vigga
        }
        enum Email
        {
            yahoo,
            gmail,
            mail
        }
        enum Domain
        {
            dk,
            com
        }
        enum MeterSeries
        {
            ES,
            KT,
            BR,
            VR,
            OT,
            PL,
            YT,
            HG,
            OL
        }
        public enum Months
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }
        public void FillDataDB()
        {
            //FillUnits();
            //FillBuildingImages();
            //FillUtilities();
            //FillUtilityIconImages();
            //FillAddresses();
            //FillCustomerTypes();
            //FillCustomers();
            //FillTemperatures();
            //FillMeterTypes();
            //FillMeters();
            //FillMeterItems();
            //FillBills();
            //UpdateBuildingImage();
            //FillBuildingImagesManually();
            //FillNewsImages();
            //FillNews();
            //FillNewsChapters(); 
        }

        public void FillAddresses()
        {
            FillTowns();
            FillIndexes();
            FillStreets();
            FillBuildings();
            FillFlatsParts();
            FillAddressTable();
        }
        public void FillCustomerTypes()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.CUSTOMER_TYPE.Any())
                {
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Apartment" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "House" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Shop" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Hotel" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Restaurant" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Cafe" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Hospital" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "School" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "WorkShop" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Company" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Church" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Administrative building" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Factory" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Plant" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Nursery" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Kinder garden" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Service station" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Fuel station" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Salon" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Museum" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Theatre" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Building" });
                    context.CUSTOMER_TYPE.Add(new CUSTOMER_TYPE() { NAME = "Other" });
                    context.SaveChanges();
                }
            }
        }
        public void FillCustomers()
        {
            FillCustomersWithImages();
            FillCustomersWithOutImages();
        }
        public string CreateSurname()
        {
            int surnamesQty = 90;
            return Enum.GetName(typeof(Surnames), rnd.Next(surnamesQty));
        }
        public string CreateName()
        {
            int namesQty = 70;
            return Enum.GetName(typeof(Names), rnd.Next(namesQty));
        }
        public string CreateMeterNumber()
        {
            int seriesQty = Enum.GetNames(typeof(MeterSeries)).Length;
            int minNumber = 100000;
            int maxNumber = 999999;

            return Enum.GetName(typeof(MeterSeries), rnd.Next(seriesQty)) + rnd.Next(minNumber, maxNumber).ToString();
        }
        public string CreateEmail(string surname, string name)
        {
            return String.Concat(surname.ToLower(), name.ToLower(), "@", Enum.GetName(typeof(Email), rnd.Next(3)), ".", Enum.GetName(typeof(Domain), rnd.Next(2)));
        }
        public string CreatePhone()
        {
            string phone = "";
            string codePhone = "+45";
            int maxPhoneNumber = 99999999;

            phone = String.Concat(codePhone, rnd.Next(maxPhoneNumber));
            for (; phone.Length < 11;)
                phone += rnd.Next(9).ToString();

            return phone;
        }
        public void FillCustomersWithOutImages()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                string surname;
                string name;
                List<int> linkedAddressIds = new List<int>();
                int customerTypeCount = context.CUSTOMER_TYPE.Count();

                var customersWithImagesDB = context.CUSTOMER.ToList();

                foreach (var c in customersWithImagesDB)
                    linkedAddressIds.Add(c.ADDRESS_ID);

                var emptyAddressesDB = context.ADDRESS.ToList();

                foreach (var a in emptyAddressesDB)
                {
                    if (!linkedAddressIds.Contains(a.ID))
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = a.ID,
                            CUSTOMER_TYPE_ID = rnd.Next(1, customerTypeCount)
                        });
                    }
                }
                context.SaveChanges();
            }
        }
        public void FillCustomersWithImages()
        {
            string surname;
            string name;

            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.CUSTOMER.Any())
                {
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Marios Pizza & Pasta", EMAIL = "mariopp@com.dk", PHONE = CreatePhone(), ADDRESS_ID = 10, CUSTOMER_TYPE_ID = 5 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Sonja Rosing Massage", EMAIL = "sonjarosing@rose.dk", PHONE = CreatePhone(), ADDRESS_ID = 12, CUSTOMER_TYPE_ID = 19 });

                    for (int i = 22; i < 26; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Idea Scum Studio", EMAIL = "ideascum@com.dk", PHONE = CreatePhone(), ADDRESS_ID = 26, CUSTOMER_TYPE_ID = 3 });

                    for (int i = 28; i < 32; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }
                    for (int i = 44; i < 50; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Star Nails", EMAIL = "starnails@com.dk", PHONE = CreatePhone(), ADDRESS_ID = 50, CUSTOMER_TYPE_ID = 19 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "J BY J HairCutter", EMAIL = "jbyj@mail.com", PHONE = CreatePhone(), ADDRESS_ID = 51, CUSTOMER_TYPE_ID = 19 });

                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Sanistaal S Company", EMAIL = "office@sscom.dk", PHONE = CreatePhone(), ADDRESS_ID = 90, CUSTOMER_TYPE_ID = 10 });
                    for (int i = 93; i < 95; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }

                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Premier", EMAIL = "kolding@premier.dk", PHONE = CreatePhone(), ADDRESS_ID = 104, CUSTOMER_TYPE_ID = 19 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Aldi", EMAIL = "kolding@aldi.dk", PHONE = CreatePhone(), ADDRESS_ID = 115, CUSTOMER_TYPE_ID = 3 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Shell", EMAIL = "kolding@shell.dk", PHONE = CreatePhone(), ADDRESS_ID = 117, CUSTOMER_TYPE_ID = 18 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Fakta", EMAIL = "kolding@fakta.dk", PHONE = CreatePhone(), ADDRESS_ID = 119, CUSTOMER_TYPE_ID = 3 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Longso Biler", EMAIL = "longso@biler.dk", PHONE = CreatePhone(), ADDRESS_ID = 132, CUSTOMER_TYPE_ID = 10 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Q8", EMAIL = "q8kolding@petrol.dk", PHONE = CreatePhone(), ADDRESS_ID = 135, CUSTOMER_TYPE_ID = 18 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Netto", EMAIL = "kolding@netto.dk", PHONE = CreatePhone(), ADDRESS_ID = 139, CUSTOMER_TYPE_ID = 3 });

                    surname = CreateSurname();
                    name = CreateName();
                    context.CUSTOMER.Add(new CUSTOMER()
                    {
                        ACCOUNT = (Account++).ToString(),
                        SURNAME = surname,
                        NAME = name,
                        EMAIL = CreateEmail(surname, name),
                        PHONE = CreatePhone(),
                        ADDRESS_ID = 205,
                        CUSTOMER_TYPE_ID = 9
                    });

                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Individuals With Asperger Syndrome", EMAIL = "hospitalasperger@org.dk", PHONE = CreatePhone(), ADDRESS_ID = 206, CUSTOMER_TYPE_ID = 7 });

                    surname = CreateSurname();
                    name = CreateName();
                    context.CUSTOMER.Add(new CUSTOMER()
                    {
                        ACCOUNT = (Account++).ToString(),
                        SURNAME = surname,
                        NAME = name,
                        EMAIL = CreateEmail(surname, name),
                        PHONE = CreatePhone(),
                        ADDRESS_ID = 207,
                        CUSTOMER_TYPE_ID = 2
                    });

                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Hostel For Individuals With Asperger Syndrome", EMAIL = "hostelasperger@org.dk", PHONE = CreatePhone(), ADDRESS_ID = 208, CUSTOMER_TYPE_ID = 22 });

                    surname = CreateSurname();
                    name = CreateName();
                    context.CUSTOMER.Add(new CUSTOMER()
                    {
                        ACCOUNT = (Account++).ToString(),
                        SURNAME = surname,
                        NAME = name,
                        EMAIL = CreateEmail(surname, name),
                        PHONE = CreatePhone(),
                        ADDRESS_ID = 209,
                        CUSTOMER_TYPE_ID = 2
                    });

                    for (int i = 210; i < 215; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }

                    surname = CreateSurname();
                    name = CreateName();
                    context.CUSTOMER.Add(new CUSTOMER()
                    {
                        ACCOUNT = (Account++).ToString(),
                        SURNAME = surname,
                        NAME = name,
                        EMAIL = CreateEmail(surname, name),
                        PHONE = CreatePhone(),
                        ADDRESS_ID = 216,
                        CUSTOMER_TYPE_ID = 2
                    });

                    for (int i = 222; i < 226; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }


                    for (int i = 227; i < 231; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }

                    for (int i = 232; i < 236; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }

                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Jens Jacobsen AutoVaerksted", EMAIL = "jensjacobsenr@yahoo.dk", PHONE = CreatePhone(), ADDRESS_ID = 267, CUSTOMER_TYPE_ID = 17 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Audi Centre", EMAIL = "odense@audi.dk", PHONE = CreatePhone(), ADDRESS_ID = 705, CUSTOMER_TYPE_ID = 3 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Statoil", EMAIL = "odensemdfvej@statoil.dk", PHONE = CreatePhone(), ADDRESS_ID = 708, CUSTOMER_TYPE_ID = 3 });

                    for (int i = 720; i < 728; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }

                    for (int i = 729; i < 737; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }

                    surname = CreateSurname();
                    name = CreateName();
                    context.CUSTOMER.Add(new CUSTOMER()
                    {
                        ACCOUNT = (Account++).ToString(),
                        SURNAME = surname,
                        NAME = name,
                        EMAIL = CreateEmail(surname, name),
                        PHONE = CreatePhone(),
                        ADDRESS_ID = 778,
                        CUSTOMER_TYPE_ID = 2
                    });

                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Unik Haarmode", EMAIL = "unikhaarmoder@yahoo.dk", PHONE = CreatePhone(), ADDRESS_ID = 780, CUSTOMER_TYPE_ID = 19 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Dame Herre", EMAIL = "dameherre@yahoo.dk", PHONE = CreatePhone(), ADDRESS_ID = 784, CUSTOMER_TYPE_ID = 19 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Fotex", EMAIL = "odense@fotex.dk", PHONE = CreatePhone(), ADDRESS_ID = 788, CUSTOMER_TYPE_ID = 3 });
                    context.CUSTOMER.Add(new CUSTOMER() { ACCOUNT = (Account++).ToString(), NAME = "Lund`b", EMAIL = "lundsodense@mail.dk", PHONE = CreatePhone(), ADDRESS_ID = 824, CUSTOMER_TYPE_ID = 6 });

                    for (int i = 834; i < 852; i++)
                    {
                        surname = CreateSurname();
                        name = CreateName();
                        context.CUSTOMER.Add(new CUSTOMER()
                        {
                            ACCOUNT = (Account++).ToString(),
                            SURNAME = surname,
                            NAME = name,
                            EMAIL = CreateEmail(surname, name),
                            PHONE = CreatePhone(),
                            ADDRESS_ID = i,
                            CUSTOMER_TYPE_ID = 1
                        });
                    }
                    context.SaveChanges();
                }
            }
        }
        public void FillTowns()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.TOWN.Any())
                {
                    context.TOWN.Add(new TOWN() { NAME = "Copenhagen" });
                    context.TOWN.Add(new TOWN() { NAME = "Odense" });
                    context.TOWN.Add(new TOWN() { NAME = "Kolding" });
                    context.TOWN.Add(new TOWN() { NAME = "Aarhus" });
                    context.TOWN.Add(new TOWN() { NAME = "Viborg" });
                    context.TOWN.Add(new TOWN() { NAME = "Esbjerg" });
                    context.TOWN.Add(new TOWN() { NAME = "Vejle" });
                    context.TOWN.Add(new TOWN() { NAME = "Randers" });
                    context.TOWN.Add(new TOWN() { NAME = "Thisted" });
                    context.SaveChanges();
                }
            }
        }
        public void FillIndexes()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.INDEX.Any())
                {
                    context.INDEX.Add(new INDEX() { VALUE = 1107 });
                    context.INDEX.Add(new INDEX() { VALUE = 1864 });
                    context.INDEX.Add(new INDEX() { VALUE = 2100 });
                    context.INDEX.Add(new INDEX() { VALUE = 2200 });
                    context.INDEX.Add(new INDEX() { VALUE = 2300 });
                    context.INDEX.Add(new INDEX() { VALUE = 2400 });
                    context.INDEX.Add(new INDEX() { VALUE = 2500 });
                    context.INDEX.Add(new INDEX() { VALUE = 5000 });
                    context.INDEX.Add(new INDEX() { VALUE = 5200 });
                    context.INDEX.Add(new INDEX() { VALUE = 5250 });
                    context.INDEX.Add(new INDEX() { VALUE = 5270 });
                    context.INDEX.Add(new INDEX() { VALUE = 6000 });
                    context.INDEX.Add(new INDEX() { VALUE = 8000 });
                    context.INDEX.Add(new INDEX() { VALUE = 8200 });
                    context.INDEX.Add(new INDEX() { VALUE = 8210 });
                    context.INDEX.Add(new INDEX() { VALUE = 8230 });
                    context.INDEX.Add(new INDEX() { VALUE = 8260 });
                    context.INDEX.Add(new INDEX() { VALUE = 8800 });
                    context.INDEX.Add(new INDEX() { VALUE = 8840 });
                    context.INDEX.Add(new INDEX() { VALUE = 7100 });
                    context.INDEX.Add(new INDEX() { VALUE = 8900 });
                    context.INDEX.Add(new INDEX() { VALUE = 8920 });
                    context.INDEX.Add(new INDEX() { VALUE = 8930 });
                    context.INDEX.Add(new INDEX() { VALUE = 8960 });
                    context.INDEX.Add(new INDEX() { VALUE = 7700 });
                    context.SaveChanges();
                }
            }
        }
        public void FillStreets()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.STREET.Any())
                {
                    #region KoldingStreets
                    context.STREET.Add(new STREET() { NAME = "Haderslevvej", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Ndr Ringvej", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Katrinegade", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Sdr Ringvej", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Istedvej", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Hertug Abels", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Agtrupvej", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Jernbanegade ", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Brostraede", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Sydbanegade", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Bredgade", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Kastaniealle", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Galgebjergvej", TOWN_ID = 3, INDEX_ID = 12 });
                    context.STREET.Add(new STREET() { NAME = "Drosselvej", TOWN_ID = 3, INDEX_ID = 12 });
                    #endregion

                    #region OdenseStreets
                    context.STREET.Add(new STREET() { NAME = "Rugaardsvej", TOWN_ID = 2, INDEX_ID = 8 });
                    context.STREET.Add(new STREET() { NAME = "Thomas Overskous Vej", TOWN_ID = 2, INDEX_ID = 8 });
                    context.STREET.Add(new STREET() { NAME = "Middelfartvej", TOWN_ID = 2, INDEX_ID = 9 });
                    context.STREET.Add(new STREET() { NAME = "Roesskovsvej", TOWN_ID = 2, INDEX_ID = 9 });
                    context.STREET.Add(new STREET() { NAME = "Frederiksgade", TOWN_ID = 2, INDEX_ID = 8 });
                    context.STREET.Add(new STREET() { NAME = "Nyborgvej ", TOWN_ID = 2, INDEX_ID = 8 });
                    context.STREET.Add(new STREET() { NAME = "Kochsgade", TOWN_ID = 2, INDEX_ID = 8 });
                    context.STREET.Add(new STREET() { NAME = "Windelsvej", TOWN_ID = 2, INDEX_ID = 8 });
                    context.STREET.Add(new STREET() { NAME = "Gammel Hoejmevej ", TOWN_ID = 2, INDEX_ID = 10 });
                    context.STREET.Add(new STREET() { NAME = "Assensvej", TOWN_ID = 2, INDEX_ID = 10 });
                    context.STREET.Add(new STREET() { NAME = "Sanderumvej", TOWN_ID = 2, INDEX_ID = 10 });
                    context.STREET.Add(new STREET() { NAME = "Bladstrupvej", TOWN_ID = 2, INDEX_ID = 11 });
                    #endregion

                    context.SaveChanges();
                }
            }
        }
        public void FillSquaresAndImages()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                #region AddSquaresToBuildingsWithImages
                context.BUILDING.Find(10).SQUARE = 340F;
                context.BUILDING.Find(12).SQUARE = 140F;
                context.BUILDING.Find(22).SQUARE = 322F;
                context.BUILDING.Find(24).SQUARE = 284;
                context.BUILDING.Find(37).SQUARE = 671.3F;
                context.BUILDING.Find(76).SQUARE = 532.4F;
                context.BUILDING.Find(79).SQUARE = 112.2F;
                context.BUILDING.Find(89).SQUARE = 103.8F;
                context.BUILDING.Find(100).SQUARE = 638;
                context.BUILDING.Find(102).SQUARE = 0;
                context.BUILDING.Find(104).SQUARE = 654.2F;
                context.BUILDING.Find(117).SQUARE = 281.9F;
                context.BUILDING.Find(120).SQUARE = 164.3F;
                context.BUILDING.Find(124).SQUARE = 598.5F;
                context.BUILDING.Find(190).SQUARE = 190.4F;
                context.BUILDING.Find(191).SQUARE = 1084F;
                context.BUILDING.Find(192).SQUARE = 94;
                context.BUILDING.Find(193).SQUARE = 838;
                context.BUILDING.Find(194).SQUARE = 103.2F;
                context.BUILDING.Find(196).SQUARE = 88.3F;
                context.BUILDING.Find(239).SQUARE = 193.8F;
                context.BUILDING.Find(676).SQUARE = 312;
                context.BUILDING.Find(679).SQUARE = 181.2F;
                context.BUILDING.Find(735).SQUARE = 96;
                context.BUILDING.Find(737).SQUARE = 93;
                context.BUILDING.Find(741).SQUARE = 112;
                context.BUILDING.Find(745).SQUARE = 1612;
                context.BUILDING.Find(781).SQUARE = 244;
                #endregion

                #region AddImages
                context.BUILDING.Find(10).IMAGE_ID = 1;
                context.BUILDING.Find(12).IMAGE_ID = 2;
                context.BUILDING.Find(22).IMAGE_ID = 3;
                context.BUILDING.Find(24).IMAGE_ID = 4;
                context.BUILDING.Find(37).IMAGE_ID = 5;
                context.BUILDING.Find(76).IMAGE_ID = 6;
                context.BUILDING.Find(79).IMAGE_ID = 7;
                context.BUILDING.Find(89).IMAGE_ID = 8;
                context.BUILDING.Find(100).IMAGE_ID = 9;
                context.BUILDING.Find(102).IMAGE_ID = 10;
                context.BUILDING.Find(104).IMAGE_ID = 11;
                context.BUILDING.Find(117).IMAGE_ID = 12;
                context.BUILDING.Find(120).IMAGE_ID = 13;
                context.BUILDING.Find(124).IMAGE_ID = 14;
                context.BUILDING.Find(190).IMAGE_ID = 15;
                context.BUILDING.Find(191).IMAGE_ID = 16;
                context.BUILDING.Find(192).IMAGE_ID = 17;
                context.BUILDING.Find(193).IMAGE_ID = 18;
                context.BUILDING.Find(194).IMAGE_ID = 19;
                context.BUILDING.Find(195).IMAGE_ID = 20;
                context.BUILDING.Find(196).IMAGE_ID = 21;
                context.BUILDING.Find(202).IMAGE_ID = 22;
                context.BUILDING.Find(204).IMAGE_ID = 23;
                context.BUILDING.Find(206).IMAGE_ID = 24;
                context.BUILDING.Find(239).IMAGE_ID = 25;
                context.BUILDING.Find(676).IMAGE_ID = 26;
                context.BUILDING.Find(679).IMAGE_ID = 27;
                context.BUILDING.Find(691).IMAGE_ID = 28;
                context.BUILDING.Find(693).IMAGE_ID = 29;
                context.BUILDING.Find(735).IMAGE_ID = 30;
                context.BUILDING.Find(737).IMAGE_ID = 31;
                context.BUILDING.Find(741).IMAGE_ID = 32;
                context.BUILDING.Find(745).IMAGE_ID = 33;
                context.BUILDING.Find(781).IMAGE_ID = 34;
                context.BUILDING.Find(791).IMAGE_ID = 35;
                #endregion

                context.SaveChanges();

                #region AddSquaresToBuildingsWithOutImages

                Random rnd = new Random();
                int minSq = 86;
                int maxSq = 470;

                foreach (var building in context.BUILDING.ToList())
                {
                    if (building.SQUARE == null)
                    {
                        building.SQUARE = rnd.Next(minSq, maxSq);
                    }
                }
                context.SaveChanges();
                #endregion
            }
        }
        public void FillBuildings()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.BUILDING.Any())
                {
                    int buildingCount;

                    #region KoldingBuildings
                    buildingCount = 189;
                    for (int i = 1; i <= buildingCount; i++)
                        context.BUILDING.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 1 });

                    buildingCount = 87;
                    for (int i = 1; i <= buildingCount; i++)
                        context.BUILDING.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 2 });

                    buildingCount = 37;
                    for (int i = 1; i <= buildingCount; i++)
                        context.BUILDING.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 3 });

                    buildingCount = 30;
                    for (int i = 1; i <= buildingCount; i++)
                        context.BUILDING.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 10 });

                    buildingCount = 32;
                    for (int i = 1; i <= buildingCount; i++)
                        context.BUILDING.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 11 });
                    #endregion

                    #region OdenseBuildings

                    buildingCount = 251;
                    for (int i = 1; i <= buildingCount; i++)
                        context.BUILDING.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 15 });

                    buildingCount = 226;
                    for (int i = 1; i <= buildingCount; i++)
                        context.BUILDING.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 17 });

                    buildingCount = 150;
                    for (int i = 1; i <= buildingCount; i++)
                        context.BUILDING.Add(new BUILDING() { NUMBER = i.ToString(), STREET_ID = 18 });
                    #endregion

                    context.SaveChanges();
                }
            }
            FillSquaresAndImages();
        }
        public void UpdateBuildingSquares()
        {
            float factor = 1.07F;
            using (var context = new TownUtilityBillSystemEntities())
            {
                HashSet<int> arrayBuildingsWithFlats = new HashSet<int>();

                foreach (var flat in context.FLAT_PART.ToList())
                {
                    arrayBuildingsWithFlats.Add(flat.BUILDING_ID);
                }

                float buildingSquare;

                foreach (var building in arrayBuildingsWithFlats)
                {
                    buildingSquare = 0;

                    foreach (var flat in context.FLAT_PART.Where(f => f.BUILDING_ID == building).ToList())
                    {
                        buildingSquare += (float)flat.SQUARE;
                    }
                    buildingSquare *= factor;
                    context.BUILDING.Find(building).SQUARE = buildingSquare;
                }
                context.SaveChanges();
            }
        }
        public void UpdateBuildingImage()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                var imagesDB = context.IMAGEBUILDING.ToList();
                foreach (var item in imagesDB)                
                    item.PATH = item.PATH.Replace("Building", "Buildings");                   
                
                context.SaveChanges();
            }
        }
        public void FillBuildingImagesManually()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.IMAGEBUILDING.Any())
                {
                    string imagePath;

                    int[] koldingHaderslevvejBuildingNumbers = new int[14] { 10, 12, 22, 24, 37, 76, 79, 89, 100, 102, 104, 117, 120, 124 };
                    int[] koldingNdrRingvejBuildingNumbers = new int[11] { 1, 2, 3, 4, 5, 6, 7, 13, 15, 17, 49 };
                    int[] odenseMiddelfartvejBuildingNumbers = new int[10] { 50, 53, 65, 67, 109, 111, 115, 119, 155, 165 };

                    foreach (var item in koldingHaderslevvejBuildingNumbers)
                    {
                        imagePath = @"\Images\TownKoldingBuildings\Kolding_Haderslevvej_" + item + ".JPG";
                        context.IMAGEBUILDING.Add(new IMAGEBUILDING() { PATH = imagePath});
                    }

                    foreach (var item in koldingNdrRingvejBuildingNumbers)
                    {
                        imagePath = @"\Images\TownKoldingBuildings\Kolding_Ndr_Ringvej_" + item + ".JPG";
                        context.IMAGEBUILDING.Add(new IMAGEBUILDING() { PATH = imagePath });
                    }

                    foreach (var item in odenseMiddelfartvejBuildingNumbers)
                    {
                        imagePath = @"\Images\TownKoldingBuildings\Odense_Middelfartvej_" + item + ".JPG";
                        context.IMAGEBUILDING.Add(new IMAGEBUILDING() { PATH = imagePath });
                    }
                    context.SaveChanges();
                }
            }
        }
        public void FillFlatsParts()
        {
            Random rnd = new Random();
            int minSq = 69;
            int maxSq = 115;

            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.FLAT_PART.Any())
                {
                    int flatCount;

                    #region KoldingFlats

                    flatCount = 4;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 22, SQUARE = rnd.Next(minSq, maxSq) });
                    context.FLAT_PART.Add(new FLAT_PART() { NAME = "Idea Scum Studio", BUILDING_ID = 22, SQUARE = rnd.Next(minSq * 2, maxSq * 2) });

                    flatCount = 4;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 24, SQUARE = rnd.Next(minSq, maxSq) });

                    flatCount = 6;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 37, SQUARE = rnd.Next(minSq, maxSq) });
                    context.FLAT_PART.Add(new FLAT_PART() { NAME = "Star Nails", BUILDING_ID = 37, SQUARE = rnd.Next(minSq * 2, maxSq * 2) });
                    context.FLAT_PART.Add(new FLAT_PART() { NAME = "J BY J HairCutter", BUILDING_ID = 37, SQUARE = rnd.Next(minSq * 2, maxSq * 2) });

                    flatCount = 2;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 79, SQUARE = rnd.Next(minSq, maxSq) });

                    flatCount = 6;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 195, SQUARE = rnd.Next(minSq, maxSq) });

                    flatCount = 4;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 202, SQUARE = rnd.Next(minSq, maxSq) });

                    flatCount = 4;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 204, SQUARE = rnd.Next(minSq, maxSq) });

                    flatCount = 4;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 206, SQUARE = rnd.Next(minSq, maxSq) });

                    flatCount = 8;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 691, SQUARE = rnd.Next(minSq, maxSq) });

                    flatCount = 8;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 693, SQUARE = rnd.Next(minSq, maxSq) });

                    flatCount = 18;
                    for (int i = 1; i <= flatCount; i++)
                        context.FLAT_PART.Add(new FLAT_PART() { NUMBER = i, BUILDING_ID = 791, SQUARE = rnd.Next(minSq, maxSq) });
                    #endregion
                    context.SaveChanges();
                }
            }
            UpdateBuildingSquares();
        }
        public void FillAddressTable()
        {
            int i, j;

            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.ADDRESS.Any())
                {
                    #region KoldingHaderslevvej

                    for (i = 1, j = 22; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = i });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 22, FLAT_PART_ID = 1 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 22, FLAT_PART_ID = 2 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 22, FLAT_PART_ID = 3 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 22, FLAT_PART_ID = 4 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 22, FLAT_PART_ID = 5 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 23 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 24, FLAT_PART_ID = 6 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 24, FLAT_PART_ID = 7 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 24, FLAT_PART_ID = 8 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 24, FLAT_PART_ID = 9 });
                    for (i = 25, j = 37; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = i });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 10 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 11 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 12 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 13 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 14 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 15 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 16 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 37, FLAT_PART_ID = 17 });
                    for (i = 38, j = 79; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = i });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 79, FLAT_PART_ID = 18 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = 79, FLAT_PART_ID = 19 });
                    for (i = 80, j = 190; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 1, BUILDING_ID = i });
                    #endregion

                    #region KoldingNdr_Ringvej
                    for (i = 190, j = 195; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = i });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 20 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 21 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 22 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 23 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 24 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 195, FLAT_PART_ID = 25 });
                    for (i = 196, j = 202; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = i });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 202, FLAT_PART_ID = 26 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 202, FLAT_PART_ID = 27 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 202, FLAT_PART_ID = 28 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 202, FLAT_PART_ID = 29 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 203 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 204, FLAT_PART_ID = 30 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 204, FLAT_PART_ID = 31 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 204, FLAT_PART_ID = 32 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 204, FLAT_PART_ID = 33 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 205 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 206, FLAT_PART_ID = 34 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 206, FLAT_PART_ID = 35 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 206, FLAT_PART_ID = 36 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 206, FLAT_PART_ID = 37 });
                    for (i = 207, j = 239; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = i });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = 239 });
                    for (i = 240, j = 277; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 2, BUILDING_ID = i });
                    #endregion

                    #region KoldingOtherStreets
                    for (i = 277, j = 314; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 3, BUILDING_ID = i });
                    for (i = 314, j = 344; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 10, BUILDING_ID = i });
                    for (i = 344, j = 376; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 12, TOWN_ID = 3, STREET_ID = 11, BUILDING_ID = i });
                    #endregion

                    #region OdenseStreets
                    for (i = 376, j = 627; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 15, BUILDING_ID = i });

                    for (i = 627, j = 691; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = i });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 38 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 39 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 40 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 41 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 42 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 43 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 44 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 691, FLAT_PART_ID = 45 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 692 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 46 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 47 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 48 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 49 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 50 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 51 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 52 });
                    context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 693, FLAT_PART_ID = 53 });
                    for (i = 694, j = 791; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = i });
                    for (int n = 54; n < 72; n++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = 791, FLAT_PART_ID = n });
                    for (i = 792, j = 854; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 17, BUILDING_ID = i });
                    for (i = 854, j = 1003; i < j; i++)
                        context.ADDRESS.Add(new ADDRESS() { INDEX_ID = 9, TOWN_ID = 2, STREET_ID = 18, BUILDING_ID = i });
                    #endregion
                    context.SaveChanges();
                }
            }
        }
        public void FillUnits()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.UNIT.Any())
                {
                    context.UNIT.Add(new UNIT() { NAME = "kWh" });
                    context.UNIT.Add(new UNIT() { NAME = "cub.m" });
                    context.UNIT.Add(new UNIT() { NAME = "Gcal" });
                    context.UNIT.Add(new UNIT() { NAME = "cub.m" });
                    context.UNIT.Add(new UNIT() { NAME = "°C" });
                    context.UNIT.Add(new UNIT() { NAME = "DKK" });
                    context.SaveChanges();
                }
            }
        }
        
        public void FillUtilities()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.UTILITY.Any())
                {
                    context.UTILITY.Add(new UTILITY() { NAME = "Electricity", PRICE = 2.24m, UNIT_ID = 1, USAGEFORSTANDARTPRICE = 200, BIGUSAGEPRICE = 2.8m, IMAGE_ID = 1 });
                    context.UTILITY.Add(new UTILITY() { NAME = "Water", PRICE = 15.98m, UNIT_ID = 2, IMAGE_ID = 2 });
                    context.UTILITY.Add(new UTILITY() { NAME = "Heating", PRICE = 482.68m, UNIT_ID = 3, IMAGE_ID = 3 });
                    context.UTILITY.Add(new UTILITY() { NAME = "Gas", PRICE = 12.48m, UNIT_ID = 4, IMAGE_ID = 4 });
                    context.SaveChanges();
                }
            }
        }
        public void FillMeterTypes()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.METER_TYPE.Any())
                {
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "Itron CENTRON", VARIFICATION_PERIOD_YEARS = 4, UTILITY_ID = 1 });
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "LANDIS & GYR (L&G), type ALF", VARIFICATION_PERIOD_YEARS = 3, UTILITY_ID = 1 });
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "General Electric I210+", VARIFICATION_PERIOD_YEARS = 5, UTILITY_ID = 1 });
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "Schneider Electric PM1200", VARIFICATION_PERIOD_YEARS = 5, UTILITY_ID = 1 });
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "NEPTUNE E-Coder", VARIFICATION_PERIOD_YEARS = 3, UTILITY_ID = 2 });
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "ULTRASONIC STUF-280W", VARIFICATION_PERIOD_YEARS = 3, UTILITY_ID = 2 });
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "Elster Ultrasonic F96", VARIFICATION_PERIOD_YEARS = 4, UTILITY_ID = 3 });
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "Ista C03P", VARIFICATION_PERIOD_YEARS = 4, UTILITY_ID = 3 });
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "Multical 302", VARIFICATION_PERIOD_YEARS = 3, UTILITY_ID = 3 });
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "TaleXus ACE9000 KBD", VARIFICATION_PERIOD_YEARS = 4, UTILITY_ID = 4 });
                    context.METER_TYPE.Add(new METER_TYPE() { NAME = "Kita FDC-4000", VARIFICATION_PERIOD_YEARS = 3, UTILITY_ID = 4 });

                    context.SaveChanges();
                }
            }
        }
        public void FillMeters()
        {
            Dictionary<int, int> varifPeriods = new Dictionary<int, int>();
            DateTime releaseDate = new DateTime();
            DateTime varifDate = new DateTime();
            int meterTypeId;
            int meterElQty = 0;
            int meterWaterQty = 0;
            int meterHeatQty = 0;
            int meterGasQty = 0;

            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.METER.Any())
                {
                    List<int> idsBuildingDB = new List<int>();
                    List<int> idsAddressesWithFourMeters = new List<int>();
                    List<int> idsAddressesWithNotAllMeters = new List<int>();
                    var meterTypesDB = context.METER_TYPE.ToList();
                    var buildingsWithImagesDB = context.BUILDING.Where(b => b.IMAGE_ID.HasValue).ToList();
                    var addressesDB = context.ADDRESS.ToList();
                    int minMeterQty = 2;
                    int maxMeterQty = 4;
                    int meterQty;

                    foreach (var type in meterTypesDB)
                    {
                        varifPeriods.Add(type.ID, type.VARIFICATION_PERIOD_YEARS);

                        if (type.UTILITY_ID == (int)Utilities.Electricity)
                            meterElQty++;
                        else if (type.UTILITY_ID == (int)Utilities.Water)
                            meterWaterQty++;
                        else if (type.UTILITY_ID == (int)Utilities.Heating)
                            meterHeatQty++;
                        else
                            meterGasQty++;
                    }

                    int utilIdOneIndexes = meterElQty;
                    int utilIdTwoIndexes = utilIdOneIndexes + meterWaterQty;
                    int utilIdThreeIndexes = utilIdTwoIndexes + meterHeatQty;
                    int utilIdFourIndexes = utilIdThreeIndexes + meterGasQty;

                    foreach (var b in buildingsWithImagesDB)
                        idsBuildingDB.Add(b.ID);

                    foreach (var a in addressesDB)
                    {
                        if (idsBuildingDB.Contains(a.BUILDING_ID))
                            idsAddressesWithFourMeters.Add(a.ID);
                        else
                            idsAddressesWithNotAllMeters.Add(a.ID);
                    }

                    maxMeterQty += 1;

                    #region AddressesWithFourMeters
                    foreach (var a in idsAddressesWithFourMeters)
                    {
                        for (int j = 1; j < maxMeterQty; j++)
                        {
                            meterTypeId = GetRandomMeterTypeId(j, utilIdOneIndexes, utilIdTwoIndexes, utilIdThreeIndexes, utilIdFourIndexes);
                            releaseDate = GetRandomReleaseDate();
                            varifDate = GetVarificationDate(releaseDate, varifPeriods, meterTypeId);

                            context.METER.Add(new METER()
                            {
                                SERIAL_NUMBER = CreateMeterNumber(),
                                ADDRESS_ID = a,
                                METER_TYPE_ID = meterTypeId,
                                RELEASE_DATE = releaseDate,
                                VARIFICATION_DATE = varifDate
                            });
                        }
                    }
                    #endregion

                    #region AddressesWithNotAllMeters
                    foreach (var a in idsAddressesWithNotAllMeters)
                    {
                        meterQty = rnd.Next(minMeterQty, maxMeterQty);
                        meterQty++;

                        for (int j = 1; j < meterQty; j++)
                        {
                            meterTypeId = GetRandomMeterTypeId(j, utilIdOneIndexes, utilIdTwoIndexes, utilIdThreeIndexes, utilIdFourIndexes);
                            releaseDate = GetRandomReleaseDate();
                            varifDate = GetVarificationDate(releaseDate, varifPeriods, meterTypeId);

                            context.METER.Add(new METER()
                            {
                                SERIAL_NUMBER = CreateMeterNumber(),
                                ADDRESS_ID = a,
                                METER_TYPE_ID = meterTypeId,
                                RELEASE_DATE = releaseDate,
                                VARIFICATION_DATE = varifDate
                            });
                        }
                    }
                    #endregion
                    context.SaveChanges();
                }
            }
        }
        public DateTime GetRandomReleaseDate()
        {
            DateTime releaseDate = new DateTime();
            Dictionary<int, int> daysInMonth = new Dictionary<int, int>();
            int year, month, day;
            int minYear = 2011;
            int maxYear = 2013;
            int minMonth = 1;
            int maxMonth = 12;

            daysInMonth.Add(1, 31);
            daysInMonth.Add(2, 28);
            daysInMonth.Add(3, 31);
            daysInMonth.Add(4, 30);
            daysInMonth.Add(5, 31);
            daysInMonth.Add(6, 30);
            daysInMonth.Add(7, 31);
            daysInMonth.Add(8, 31);
            daysInMonth.Add(9, 30);
            daysInMonth.Add(10, 31);
            daysInMonth.Add(11, 30);
            daysInMonth.Add(12, 31);

            year = rnd.Next(minYear, maxYear);
            month = rnd.Next(minMonth, maxMonth);
            day = rnd.Next(minMonth, daysInMonth[month]);

            releaseDate = new DateTime(year, month, day);

            return releaseDate;
        }
        public DateTime GetVarificationDate(DateTime releaseDate, Dictionary<int, int> varifPeriods, int meterTypeId)
        {
            DateTime varifDate = new DateTime();
            int minDay = 1;
            int rndMaxDay = 8;

            varifDate = releaseDate.AddYears(varifPeriods[meterTypeId]).AddDays(rnd.Next(minDay, rndMaxDay));
            if (varifDate > DateTime.Now)
                varifDate = releaseDate;
            return varifDate;
        }
        public int GetRandomMeterTypeId(int j, int utilIdOneIndexes, int utilIdTwoIndexes, int utilIdThreeIndexes, int utilIdFourIndexes)
        {
            int meterTypeId;

            if (j == (int)Utilities.Electricity)
                meterTypeId = rnd.Next(1, ++utilIdOneIndexes);
            else if (j == (int)Utilities.Water)
                meterTypeId = rnd.Next(++utilIdOneIndexes, ++utilIdTwoIndexes);
            else if (j == (int)Utilities.Heating)
                meterTypeId = rnd.Next(++utilIdTwoIndexes, ++utilIdThreeIndexes);
            else
                meterTypeId = rnd.Next(++utilIdThreeIndexes, ++utilIdFourIndexes);
            return meterTypeId;
        }
        public string[] ArraySortByNameAndNumber(string[] ar)
        {
            Regex rgx = new Regex("([^0-9]*)([0-9]+)");
            Array.Sort(ar, (a, b) =>
            {
                var ma = rgx.Matches(a);
                var mb = rgx.Matches(b);
                for (int i = 0; i < ma.Count; ++i)
                {
                    int ret = ma[i].Groups[1].Value.CompareTo(mb[i].Groups[1].Value);
                    if (ret != 0)
                        return ret;

                    ret = int.Parse(ma[i].Groups[2].Value) - int.Parse(mb[i].Groups[2].Value);
                    if (ret != 0)
                        return ret;
                }
                return 0;
            });

            return ar;
        }
        public void FillBuildingImages()
        {
            string[] arrayKoldingBuildings = Directory.GetFiles(@"\Images\TownKoldingBuildings\");
            string[] arrayOdenseBuildings = Directory.GetFiles(@"\Images\TownOdenseBuildings\");

            ArraySortByNameAndNumber(arrayKoldingBuildings);
            ArraySortByNameAndNumber(arrayOdenseBuildings);

            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.IMAGEBUILDING.Any())
                {
                    foreach (string building in arrayKoldingBuildings)
                        context.IMAGEBUILDING.Add(new IMAGEBUILDING() { PATH = building });

                    foreach (string building in arrayOdenseBuildings)
                        context.IMAGEBUILDING.Add(new IMAGEBUILDING() { PATH = building });

                    context.SaveChanges();
                }
            }
        }
        public void FillUtilityIconImages()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.IMAGEUTILITY.Any())
                {
                    context.IMAGEUTILITY.Add(new IMAGEUTILITY() { PATH = @"\Images\UtilityIcons\ElectricityIcon.jpg" });
                    context.IMAGEUTILITY.Add(new IMAGEUTILITY() { PATH = @"\Images\UtilityIcons\WaterIcon.jpg" });
                    context.IMAGEUTILITY.Add(new IMAGEUTILITY() { PATH = @"\Images\UtilityIcons\HeatingIcon.jpg" });
                    context.IMAGEUTILITY.Add(new IMAGEUTILITY() { PATH = @"\Images\UtilityIcons\GasIcon.jpg" });
                    context.SaveChanges();
                }

            }
        }
        public void FillTemperatures()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                DateTime date = new DateTime();
                DateTime presentDate = DateTime.Today;
                int startYear = 2015;
                int startMonth = 11;
                int startDay = 1;
                int rndMin = -3;
                int rndMax = 3;
                int minTemp = 1;
                int maxTemp = 2;
                int month;

                int[,] averageTemperaturesPerMonth = new int[12, 3] {
                                                                        { (int)Months.January, -1 , 4 },
                                                                        { (int)Months.February, -1 , 4 },
                                                                        { (int)Months.March, 0 ,  7},
                                                                        { (int)Months.April,  3, 11},
                                                                        {(int)Months.May ,  7,  16},
                                                                        { (int)Months.June,  11,  19},
                                                                        { (int)Months.July,  13,  22},
                                                                        { (int)Months.August,  13,  22},
                                                                        { (int)Months.September,  10,  17},
                                                                        { (int)Months.October,  6,  12},
                                                                        { (int)Months.November,  3, 7 },
                                                                        { (int)Months.December,  0, 5 },
                                                                    };
                int rowLength = averageTemperaturesPerMonth.GetLength(0);
                int colLength = averageTemperaturesPerMonth.GetLength(1);

                var townsDB = context.TOWN.ToList();

                if (!context.TEMPERATURE.Any())
                {
                    foreach (var t in townsDB)
                    {
                        date = new DateTime(startYear, startMonth, startDay);

                        for (; date < presentDate; date = date.AddDays(1))
                        {
                            month = date.Month - 1;
                            context.TEMPERATURE.Add(new TEMPERATURE()
                            {
                                DATE = date,
                                TOWN_ID = t.ID,
                                MINVALUE = averageTemperaturesPerMonth[month, minTemp] + rnd.Next(rndMin, rndMax),
                                MAXVALUE = averageTemperaturesPerMonth[month, maxTemp] + rnd.Next(rndMin, rndMax)
                            });
                        }
                        context.SaveChanges();
                    }
                }
            }
        }
        public float GetRandomDoubleNumber(float minimum, float maximum)
        {
            return (float)rnd.NextDouble() * (maximum - minimum) + minimum;
        }
        public float GetUtilityUsagePerSqMeter(int utility)
        {
            float utilityUsagePerSqMeter;

            if (utility == (int)Utilities.Electricity)
                utilityUsagePerSqMeter = 2.72F;
            else if (utility == (int)Utilities.Water)
                utilityUsagePerSqMeter = 0.11F;
            else if (utility == (int)Utilities.Heating)
                utilityUsagePerSqMeter = 0.013F;
            else
                utilityUsagePerSqMeter = 0.075F;

            return utilityUsagePerSqMeter;
        }
        public float GetHeatingMonthFactor(DateTime date, int townId)
        {
            int helpFactor1 = 9;
            int helpFactor2 = 10;
            using (var context = new TownUtilityBillSystemEntities())
            {
                List<Temperature> temperaturesDB = new List<Temperature>();
                DateTime temperatureStartDate = new DateTime();
                DateTime temperatureFinishDate = new DateTime();
                temperatureFinishDate = date;
                temperatureStartDate = date.AddMonths(-1);

                float heatingMonthFactor;
                int month;

                if (date.Month != (int)Months.January)
                    month = date.Month - 1;
                else
                    month = (int)Months.December;


                float valueSum = 0;
                float averageValue;
                int daysInMonth = System.DateTime.DaysInMonth(temperatureStartDate.Year, temperatureStartDate.Month);

                var temperatureItemsDB = context.TEMPERATURE.Where(t => t.TOWN_ID == townId).ToList();

                foreach (var d in temperatureItemsDB)
                    temperaturesDB.Add(new Temperature() { Id = d.ID, Date = d.DATE, MinValue = d.MINVALUE, MaxValue = d.MAXVALUE });

                for (; temperatureStartDate < temperatureFinishDate; temperatureStartDate = temperatureStartDate.AddDays(1))
                    valueSum += (float)(temperaturesDB.Where(t => t.Date == temperatureStartDate).FirstOrDefault().MinValue + temperaturesDB.Where(t => t.Date == temperatureStartDate).FirstOrDefault().MaxValue) / 2;

                averageValue = valueSum / daysInMonth;

                if (month >= (int)Months.May && month <= (int)Months.September)
                    heatingMonthFactor = 0;
                else
                    heatingMonthFactor = (helpFactor2 - averageValue) / helpFactor1;

                return heatingMonthFactor;
            }
        }
        public void FillMeterItems()
        {
            DateTime date = new DateTime();
            DateTime presentDate = DateTime.Today;
            int startYear = 2016;
            int startMonth = 1;
            int startDay = 1;
            int metersWithBuildingImagesDisplayCount = 380;
            int metersWithBuildingWithOutImagesDisplayCount = 20;

            float utilityUsagePerSqMeter;

            using (var context = new TownUtilityBillSystemEntities())
            {
                List<int> idsBuildingDB = new List<int>();
                List<int> idsAddressesWithFourMeters = new List<int>();
                List<int> idsAddressesWithNotAllMeters = new List<int>();

                float minUsageFactor = 0.77F;
                float maxUsageFactor = 1.18F;
                int rndMinStartMeterValue = 2200;
                int rndMaxStartMeterValue = 8900;
                float usageFactor;
                float presMeterValue;
                float prevMeterValue;
                float heatingMonthFactor;

                var meterTypesDB = context.METER_TYPE.ToList();
                var buildingsWithImagesDB = context.BUILDING.Where(b => b.IMAGE_ID.HasValue).ToList();
                var addressesDB = context.ADDRESS.ToList();
                var metersDB = context.METER.ToList();
                var utilitiesDB = context.UTILITY.ToList();

                foreach (var b in buildingsWithImagesDB)
                    idsBuildingDB.Add(b.ID);

                foreach (var a in addressesDB)
                {
                    if (idsBuildingDB.Contains(a.BUILDING_ID))
                        idsAddressesWithFourMeters.Add(a.ID);
                    else
                        idsAddressesWithNotAllMeters.Add(a.ID);
                }

                var metersWithBuildingImagesDB = metersDB.Where(m => idsAddressesWithFourMeters.Contains(m.ADDRESS_ID)).ToList();

                var metersWithBuildingWithOutImagesDB = metersDB.Where(m => idsAddressesWithNotAllMeters.Contains(m.ADDRESS_ID)).ToList();

                //need to fill all meters
                metersWithBuildingImagesDB.RemoveRange(metersWithBuildingImagesDisplayCount, metersWithBuildingImagesDB.Count - metersWithBuildingImagesDisplayCount); // need to fill all meters data
                metersWithBuildingWithOutImagesDB.RemoveRange(metersWithBuildingWithOutImagesDisplayCount, metersWithBuildingWithOutImagesDB.Count - metersWithBuildingWithOutImagesDisplayCount);

                var metersToFill = metersWithBuildingImagesDB;
                metersToFill = metersToFill.Concat(metersWithBuildingWithOutImagesDB).ToList();

                foreach (var meter in metersToFill)
                {
                    var addressDB = context.ADDRESS.Find(meter.ADDRESS_ID);
                    var buildingDB = context.BUILDING.Find(addressDB.BUILDING_ID);
                    var meterTypeDB = context.METER_TYPE.Find(meter.METER_TYPE_ID);
                    var utilityDB = context.UTILITY.Find(meterTypeDB.UTILITY_ID);
                    float square = 0;

                    if (buildingDB.SQUARE.HasValue)
                        square = (float)buildingDB.SQUARE;

                    utilityUsagePerSqMeter = GetUtilityUsagePerSqMeter(utilityDB.ID);
                    prevMeterValue = rnd.Next(rndMinStartMeterValue, rndMaxStartMeterValue);
                    date = new DateTime(startYear, startMonth, startDay);

                    for (; date <= presentDate; date = date.AddMonths(1))
                    {
                        usageFactor = GetRandomDoubleNumber(minUsageFactor, maxUsageFactor);

                        if (utilityDB.ID == (int)Utilities.Heating)
                        {
                            heatingMonthFactor = GetHeatingMonthFactor(date, meter.ADDRESS.TOWN_ID);
                            presMeterValue = (float)Convert.ToDouble(square * utilityUsagePerSqMeter * heatingMonthFactor);
                        }
                        else
                            presMeterValue = (float)Convert.ToDouble(square * utilityUsagePerSqMeter * usageFactor);

                        prevMeterValue += presMeterValue;

                        context.METER_ITEM.Add(new METER_ITEM()
                        {
                            METER_ID = meter.ID,
                            DATE = date,
                            VALUE = prevMeterValue
                        });
                    }
                }
                context.SaveChanges();
            }
        }
        public void FillBills()
        {
            DateTime date = new DateTime();
            DateTime presentDate = DateTime.Today;
            DateTime billDate = new DateTime();
            DateTime paidMonth = new DateTime();
            DateTime monthBeforePresent = new DateTime();
            int days = presentDate.Day;
            int billsForCalculationCount = 200;
            float prevValue = 0;
            float presValue = 0;
            int startYear = 2017;
            int startMonth = 02;
            int startDay = 1;
            int minAddDay = 0;
            int maxAddDay = 2;
            string period;
            decimal sum = 0;
            bool paidFlag;
            string number;
            decimal deltaValue;

            date = new DateTime(startYear, startMonth, startDay);
            monthBeforePresent = presentDate.AddMonths(-1).AddDays(-(--days));

            using (var context = new TownUtilityBillSystemEntities())
            {
                var customersDB = context.CUSTOMER.ToList();
                customersDB.RemoveRange(billsForCalculationCount, customersDB.Count - billsForCalculationCount); // delete some customers ---> need to fill all customers data

                var utilityWithTwoPrices = context.UTILITY.Find((int)Utilities.Electricity);
                decimal minUsageForStandartPrice = (decimal)utilityWithTwoPrices.USAGEFORSTANDARTPRICE;
                decimal bigUsagePrice = (decimal)utilityWithTwoPrices.BIGUSAGEPRICE;

                for (; date < presentDate; date = date.AddMonths(1))
                {
                    billDate = date.AddDays(rnd.Next(minAddDay, maxAddDay));
                    paidMonth = date.AddMonths(-1);

                    period = paidMonth.ToString("yyyy-MM");

                    foreach (var c in customersDB)
                    {
                        ADDRESS address = new ADDRESS();
                        METER_TYPE meterType = new METER_TYPE();
                        UTILITY utility = new UTILITY();
                        var metersDB = context.METER.ToList();
                        var meterItemsDB = context.METER_ITEM.ToList();
                        var meterTypesDB = context.METER_TYPE.ToList();

                        sum = 0;

                        address = context.ADDRESS.Find(c.ADDRESS_ID);
                        var metersForAddress = metersDB.Where(meter => meter.ADDRESS_ID == address.ID).ToList();

                        foreach (var m in metersForAddress)
                        {
                            meterType = context.METER_TYPE.Find(m.METER_TYPE_ID);
                            utility = context.UTILITY.Find(meterType.UTILITY_ID);
                            prevValue = 0;
                            presValue = 0;

                            var meterItemsForExactMeter = meterItemsDB.Where(item => item.METER_ID == m.ID).ToList();

                            foreach (var item in meterItemsForExactMeter)
                            {
                                if (item.DATE == paidMonth)
                                    prevValue = item.VALUE;
                                else if (item.DATE == date)
                                    presValue = item.VALUE;
                            }
                            deltaValue = (decimal)(presValue - prevValue);

                            if (utility.NAME == Utilities.Electricity.ToString())
                            {
                                if (deltaValue > minUsageForStandartPrice)
                                    sum += (decimal)utility.PRICE * minUsageForStandartPrice + bigUsagePrice * (deltaValue - minUsageForStandartPrice);
                            }
                            else
                                sum += (decimal)utility.PRICE * deltaValue;
                        }

                        if (paidMonth == monthBeforePresent)
                            paidFlag = false;
                        else
                            paidFlag = true;

                        number = period.Replace("-", "") + "-" + c.ACCOUNT;

                        if (sum != 0)
                            context.BILL.Add(new BILL() { NUMBER = number, CUSTOMER_ID = c.ID, DATE = billDate, PERIOD = period, SUM = sum, PAID = paidFlag });
                    }
                }
                context.SaveChanges();
            }
        }
		
		private string GetEncodedKeyAndSalt(string password, out string encodedSalt)
        {
            int byteSaltSize = 20;
            string encodedKey = "";

            using (var deriveBytes = new Rfc2898DeriveBytes(password, byteSaltSize))
            {
				//add contact data -- Git branch "contact-changes"
                byte[] salt = deriveBytes.Salt;
                byte[] key = deriveBytes.GetBytes(byteSaltSize);

                encodedSalt = Convert.ToBase64String(salt);
                encodedKey = Convert.ToBase64String(key);
            }
            return encodedKey;
        }
		
        public void FillNewsImages()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.IMAGENEWS.Any())
                {                    
                    context.IMAGENEWS.Add(new IMAGENEWS() { PATH = @"\Images\News\PlantNight.jpg" });
                    context.IMAGENEWS.Add(new IMAGENEWS() { PATH = @"\Images\News\Investment.jpg" });
                    context.IMAGENEWS.Add(new IMAGENEWS() { PATH = @"\Images\News\NewBoardMember.jpg" });
                    context.IMAGENEWS.Add(new IMAGENEWS() { PATH = @"\Images\News\WindMills.jpg" });
                    context.IMAGENEWS.Add(new IMAGENEWS() { PATH = @"\Images\News\Plant.jpg" });
                    context.SaveChanges();
                }
            }
        }
        public void FillNews()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.NEWS.Any())
                {
                    context.NEWS.Add(new NEWS()
                    {
                        TITLE = "Sale of 10,509,527 existing shares in DONG Energy A/S by New Energy Investment",
                        DATE = new DateTime(2017, 07, 05, 12, 10, 00),
                        IMAGE_ID = 1
                    });
                    context.NEWS.Add(new NEWS()
                    {
                        TITLE = "Major Shareholder notification – New Energy Investment",
                        DATE = new DateTime(2017, 06, 22, 10, 11, 00),
                        IMAGE_ID = 2
                    });
                    context.NEWS.Add(new NEWS()
                    {
                        TITLE = "The Nomination Committee of DONG Energy A/S recommends new member for the Board of Directors",
                        DATE = new DateTime(2017, 06, 15, 17, 05, 00),
                        IMAGE_ID = 3
                    });
                    context.NEWS.Add(new NEWS()
                    {
                        TITLE = "DONG Energy awarded three German offshore wind projects",
                        DATE = new DateTime(2017, 04, 02, 10, 32, 00),
                        IMAGE_ID = 4
                    });
                    context.NEWS.Add(new NEWS()
                    {
                        TITLE = "Resolutions from the Annual General Meeting of DONG Energy A/S",
                        DATE = new DateTime(2017, 02, 03, 09, 45, 00),
                        IMAGE_ID = 5
                    });

                    context.SaveChanges();
                }
            }
        }
        public void FillNewsChapters()
        {
            using (var context = new TownUtilityBillSystemEntities())
            {
                if (!context.NEWSCHAPTER.Any())
                {
                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 1,
                        TEXT = "THE SECURITIES DESCRIBED HEREIN HAVE NOT BEEN AND WILL NOT BE REGISTERED UNDER THE U.S. SECURITIES ACT OF 1933, AS AMENDED, AND MAY NOT BE OFFERED OR SOLD IN THE UNITED STATES ABSENT REGISTRATION OR AN APPLICABLE EXEMPTION FROM REGISTRATION REQUIREMENTS."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 1,
                        TEXT = "With reference to the announcement made on 3 July, 2017, DONG Energy A/S (‘DONG Energy’) (NASDAQ OMX: DENERG), has received the following information from New Energy Investment S.à.r.l. (‘NEI’):"
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 1,
                        TEXT = "NEI has agreed to sell 10,509,527 existing shares in DONG Energy, equivalent to 2.5% of the existing shares in DONG Energy to institutional investors at a price of DKK 270 per share, pursuant to an accelerated bookbuild offering (the ‘Transaction’). NEI is a Luxembourg company indirectly owned by entities under the control of the Merchant Banking Division of The Goldman Sachs Group, Inc."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 1,
                        TEXT = "Following settlement of the Transaction, NEI will hold 18,935,215 shares in DONG Energy corresponding to 4.5% of the existing shares in DONG Energy."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 1,
                        TEXT = "NEI has agreed to a 90-day lock-up period from yesterday, subject to waiver with the prior written consent of a certain manager conducting the bookbuilding process and to certain customary exceptions."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 1,
                        TEXT = "DONG Energy will not receive any proceeds from the Transaction."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 2,
                        TEXT = "Pursuant to section 29 of the Danish Securities Trading Act, New Energy Investment S.à r.l. (‘NEI’) has notified DONG Energy A/S (‘DONG Energy’) that today, following an accelerated bookbuild offering carried out on 3 February 2017, NEI has agreed to dispose of 26,500,000 shares in DONG Energy to certain institutional investors, each with a nominal value of DKK 10."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 2,
                        TEXT = "Following settlement of the bookbuild offering, NEI will hold 29,444,742 shares and voting rights in DONG Energy, corresponding to 7.0% of the issued shares and voting rights of DONG Energy."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 2,
                        TEXT = "NEI is a limited liability company organised under the laws of Luxembourg under reg. no. B181487. The address of NEI is 2 Rue de Fossé, L-1536 Luxembourg, Grand Duchy of Luxembourg."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 2,
                        TEXT = "As at the date hereof, NEI is controlled by New Energy I S.à r.l. (‘NE I’) and New Energy II S.à r.l. (‘NE II’). NE I, which possesses the majority of voting rights in NEI, and NE II are limited liability companies organised under the laws of Luxembourg and are controlled by entities which are under the control of the Merchant Banking Division of The Goldman Sachs Group, Inc.. These entities include Danish Energy Investors B, L.P., a Cayman Islands limited partnership, which possesses the majority of voting rights in NE I."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 2,
                        TEXT = "The information provided in this announcement does not change DONG Energy’s previous financial guidance for the 2017 financial year."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 3,
                        TEXT = "Today, the Nomination Committee of DONG Energy A/S has decided to recommend that Dieter Wemmer be elected new member of the Board of Directors."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 3,
                        TEXT = "Dieter Wemmer has worked in leading finance positions for more than two decades, most recently as Chief Financial Officer of Allianz SE and as board member of the UBS Group AG. He is highly experienced within capital markets, investments and risk management and combines a sharp financial insight with a strategic and operational mind-set."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 3,
                        TEXT = "Thomas Thune Andersen, Chairman of the Board of Directors and the Nomination Committee of DONG Energy A/S, said:"
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 3,
                        TEXT = "“I’m pleased that the Nomination Committee has recommended Dieter Wemmer as a new member of the Board of Directors. He holds very strong financial capabilities and adds strong experience within capital markets, investments and risk management to the board. I’m confident that he’ll be an asset for the Board.”"
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 3,
                        TEXT = "Dieter Wemmer is expected to be elected for the Board of Directors at the annual general meeting in Q1 2018."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 3,
                        TEXT = "The Board of Directors of DONG Energy A/S currently consists of six members elected by the general meeting. As previously announced, the long-term objective is to have eight members of the Board of Directors elected by the general meeting."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 3,
                        TEXT = "Further information about Dieter Wemmer is enclosed below."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "In the first of two German auction rounds, the Bundesnetzagentur has today awarded DONG Energy the right to build three offshore wind projects in the German North Sea. DONG Energy submitted six projects in the bid and won with the following three projects which have a total capacity of 590MW: OWP West (240MW), Borkum Riffgrund West 2 (240MW),  Gode Wind 3 (110MW)."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "The three projects are planned to be commissioned in 2024, subject to Final Investment Decision by DONG Energy in 2021."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "Samuel Leupold, Executive Vice President and CEO of Wind Power at DONG Energy, says: “We’re pleased with being awarded three projects in the first of two German auction rounds, and we have good opportunities to add further capacity to our winning projects in next year’s German auction. Today’s results contribute to our ambition of driving profitable growth by adding approximately 5GW of additional capacity by 2025.”"
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "For two of the projects – OWP West and Borkum Riffgrund West 2 – DONG Energy made bids at zero EUR per MWh, i.e. these projects will not receive a subsidy on top of the wholesale electricity price. The Gode Wind 3 project was awarded based on a bid price of EUR 60 per MWh."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "Samuel Leupold says:“The zero subsidy bid is a breakthrough for the cost competitiveness of offshore wind, and it demonstrates the technology's massive global growth potential as a cornerstone in the economically viable shift to green energy systems. Cheaper clean energy will benefit governments and consumers – and not least help meet the Paris COP21 targets to fight climate change. Still it’s important to note that the zero bid is enabled by a number of circumstances in this auction. Most notably, the realization window is extended to 2024. This allows developers to apply the next generation turbine technology, which will support a major step down in costs. Also, the bid reflects the fact that grid connection is not included.”"
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "Samuel Leupold continues: “Financial discipline is key to us. We are of course reflecting the project’s exposure to market risk in the cost of capital applied. We see a solid value creation potential in this German project portfolio and will now begin to further mature the projects towards a Final Investment Decision (FID) in 2021.”"
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "Volker Malmen, country manager in DONG Energy Germany, says: “Making green energy cheaper than black has for years been part of DONG Energy's strategic ambition. Offshore wind is fully capable of replacing retiring power plants and to become the backbone of Germany’s energy transition, and I hope that today’s encouraging results will inspire an accelerated and higher volume build-out of offshore wind in Germany and motivate the electrification of transportation and heating.”"
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "DONG Energy will be responsible for the turbines, array cables and offshore substation, while grid operator TenneT will be responsible for construction, operation and ownership of the onshore substation and the export cable."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "DONG Energy currently has 902MW of offshore wind in operation in German waters with Gode Wind 1&2 and Borkum Riffgrund 1 and another 450MW under construction at Borkum Riffgrund 2, which is expected to be commissioned in 2019. In total, DONG Energy operates 3,600MW offshore wind capacity across Germany, UK and Denmark and has a further 3,800GW under construction."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "Platform change: Significantly bigger turbines – probably 13-15MW – will be on the market by 2024. With bigger turbines, the developer can increase electricity production while at the same time reduce the number of turbine positions. This contributes significantly to cost reductions during construction (fewer towers and array cables, and lower costs for installation vessels and manpower) as well as during a lifetime of operations and maintenance."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "Scale: OWP West and Borkum Riffgrund West 2 will be combined into one large-scale project with the option of adding additional volume in next year’s auction to further increase the total size of the project."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "Location: The projects benefit from average wind speeds of more than 10 m/s, which is among the highest wind speeds measured across DONG Energy’s portfolio of wind farms. Also, the projects are located next to DONG Energy’s Borkum Riffgrund 1&2 which means that operations and maintenance can be done from DONG Energy’s existing O&M hub in Norddeich."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "Extended lifetime: The German authorities have approved the possibility to extend the operational lifetime of the asset from 25 to 30 years."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "Not full scope: Developers were not bidding for the grid connection in the German auction, which means that grid connection is not included in the bid price."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "The above drivers deliver a cost-of-electricity below our forecasted wholesale power price and will allow us to create value and meet our return requirements at the expected market prices without subsidies. Compared to German power price forecasts available from leading research firms, we consider our price forecast to be relatively conservative. We have applied a higher cost-of-capital than in previous projects to reflect the potential increase in market price exposure. "
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "The cost reductions required for a German project without subsidies are fully feasible, both technically and commercially. Towards a final investment decision in 2021, DONG Energy will monitor the key factors which will determine long-term power prices in Germany. These factors include the impact of EU actions to reinvigorate the European carbon trading scheme; the phase-out of conventional and nuclear capacity; the future role of coal in Europe; and the build-out of onshore transmission grids."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 4,
                        TEXT = "The information provided in this announcement does not change DONG Energy’s previous financial guidance for the financial year of 2017 or the announced expected investment level for 2017."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 5,
                        TEXT = "Today, DONG Energy A/S held its Annual General Meeting where the following was adopted:"
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 5,
                        TEXT = "DONG Energy’s audited annual report 2016 was approved."
                    });


                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 5,
                        TEXT = "The distribution of profit according to the adopted annual report 2016 was approved. The dividend will amount to DKK 6 per share."
                    });


                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 5,
                        TEXT = "Thomas Thune Andersen was re-elected as Chairman of the Board of Directors, Lene Skole was re-elected as Deputy Chairman of the Board of Directors, and Lynda Armstrong, Pia Gjellerup and Benny D. Loft were re-elected as members of the Board of Directors."
                    });


                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 5,
                        TEXT = "Peter Korsholm was elected as new member of the Board of Directors."
                    });


                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 5,
                        TEXT = "PricewaterhouseCoopers was re-appointed as auditor."
                    });


                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 5,
                        TEXT = "The board remuneration for 2017 was approved."
                    });

                    context.NEWSCHAPTER.Add(new NEWSCHAPTER()
                    {
                        NEWS_ID = 5,
                        TEXT = "The Board of Directors and the Executive Board were discharged from their obligations."
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}