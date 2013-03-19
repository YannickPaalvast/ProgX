using PickTheName_db.Models;
using SilverlightPhoneDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PickTheName_db
{
    public class Silverdb
    {
        public static Database database;

        /// <summary>
        /// Controleerd of de database al bestaat. Wanneer dit niet het geval is, wordt deze aangemaakt.
        /// </summary>
        public static void checkDatabase()
        {
            if (!Database.DoesDatabaseExists(DataStrings.database_name))
            {
                database = Database.CreateDatabase(DataStrings.database_name);
                database.CreateTable<Active>();
                database.CreateTable<Name>();
                database.Save();           
            }
            else
            {
                database = Database.OpenDatabase(DataStrings.database_name, string.Empty, true);
            }

        }
        /// <summary>
        /// Er wordt een bepaald product uit de database verwijderd.
        /// </summary>
        /// <param name="index">De plaats in de database waar het product staat welke verwijderd moet worden.</param>
        public static void DeleteName(int id)
        {
            deleteActive(id);
            database = Database.OpenDatabase(DataStrings.database_name);
            
            var query = (from leeg in database.Table<Name>()
                         where leeg.id == id
                         select leeg).Single<Name>();
            

            database.Table<Name>().Remove(query);
            database.Save();
            
        }
        /// <summary>
        ///  De hele database wordt geleegd
        /// </summary>
        public static void LeegNameDB()
        {
            LeegActiveDB();
           database = Database.OpenDatabase(DataStrings.database_name);

            var query = (from leeg in database.Table<Name>()
                         select leeg);

            for (int i = (query.ToArray().Length) - 1; i >= 0; i--)
            {
                database.Table<Name>().Remove(query.ElementAt(i));
            }
            database.Save();
            
        }
        public static void LeegActiveDB()
        {
            database = Database.OpenDatabase(DataStrings.database_name);

            var query = (from leeg in database.Table<Active>()
                         select leeg);

            for (int i = (query.ToArray().Length) - 1; i >= 0; i--)
            {
                database.Table<Active>().Remove(query.ElementAt(i));
            }
            database.Save();
        }
       

        public static void AddName(string _name)
        {
            Name name = new Name() {id=int.Parse(Settings.settings[DataStrings.setting_nameid].ToString()), name = _name };
            Settings.settings[DataStrings.setting_nameid] = int.Parse(Settings.settings[DataStrings.setting_nameid].ToString())+1;
            database = Database.OpenDatabase(DataStrings.database_name);
            database.Table<Name>().Add(name);
            database.Save();
        }

        public static void AddActive(int _id)
        {
            Active act = new Active() { id = _id };
            
            database = Database.OpenDatabase(DataStrings.database_name);
            database.Table<Active>().Add(act);
            database.Save();
        }
        public static List<Name> getAllNames()
        {
            List<Name> _names = (from pn in database.Table<Name>() select new Name {id =pn.id, name = pn.name }).ToList();

            return _names;

        }


        public static List<Name> getActiveNames()
        {
            List<Name> active = new List<Name>();
            List<Active> _activeIds = (from pn in database.Table<Active>() select new Active { id = pn.id }).ToList();

            foreach(Active ac in _activeIds)
            {
                try
                {
                    Name name = (from pn in database.Table<Name>() where pn.id == ac.id select new Name { name = pn.name, id = pn.id }).Single<Name>();
                    active.Add(name);
                }
                catch { }
            }

            return active;
        }
        public static bool checkActive()
        {
            List<Active> _activeIds = (from pn in database.Table<Active>() select new Active { id = pn.id }).ToList();
            if (_activeIds.Count() < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool checkNameActive(int id)
        {
            List<Active> _activeIds = (from pn in database.Table<Active>() where pn.id==id select new Active { id = pn.id }).ToList();
            if (_activeIds.Count() < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void deleteActive(int id)
        {
            try
            {
                database = Database.OpenDatabase(DataStrings.database_name);

                var query = (from leeg in database.Table<Active>()
                             where id == leeg.id
                             select leeg).Single<Active>();

                database.Table<Active>().Remove(query);
                database.Save();
            }
            catch { }
        }
        public static int countActive()
        {
            return getActiveNames().Count();
        }

        public static string getName(int index)
        {
            Name chosen = getActiveNames().ElementAt<Name>(index);

            return chosen.name;
        }
    }
}
