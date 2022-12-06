﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata02_IEquatable_IComparable_Factory
{
    class Member : IMember
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MemberLevel Level { get; set; }
        public DateTime Since { get; set; }
        public override string ToString() => $"{FirstName} {LastName} is a {Level} member since {Since.Year}";

        #region Implement IComparable
        public int CompareTo(IMember other)
        {
            if (Level != other.Level)
                return Level.CompareTo(other.Level);

            if (LastName != other.LastName)
                return LastName.CompareTo(other.LastName);

            if (FirstName != other.FirstName)
                return FirstName.CompareTo(other.FirstName);
            
            return Since.CompareTo(other.Since);
        }
        #endregion

        #region Implement IEquatable
        public bool Equals(IMember other) => (this.FirstName, this.LastName, this.Level, this.Since) == 
            (other.FirstName, other.LastName, other.Level, other.Since);

        // legacy .NET compliance
        public override bool Equals(object obj) => Equals(obj as IMember);
        public override int GetHashCode() => (this.FirstName, this.LastName, this.Level, this.Since).GetHashCode();
        #endregion

        #region Class Factory for creating an instance filled with Random data
        public static class Factory
        {
            public static Member CreateRandom()
            {
                var rnd = new Random();
                //Alternative 1: Lazy mans alternative
                /*
                while (true)
                {
                    try
                    {
                        int year = rnd.Next(1980, DateTime.Today.Year + 1);
                        int month = rnd.Next(1, 13);
                        int day = rnd.Next(1, 31);

                        var Since = new DateTime(year, month, day);
                        var Level = (MemberLevel)rnd.Next((int)MemberLevel.Platinum, (int)MemberLevel.Blue + 1);

                        var _fnames = "Harry, Lord, Hermione, Albus, Severus, Ron, Draco, Frodo, Gandalf, Sam, Peregrin, Saruman".Split(", ");
                        var _lnames = "Potter, Voldemort, Granger, Dumbledore, Snape, Malfoy, Baggins, the Gray, Gamgee, Took, the White".Split(", ");
                        var FirstName = _fnames[rnd.Next(0, _fnames.Length)];
                        var LastName = _lnames[rnd.Next(0, _lnames.Length)];

                        var member = new Member { FirstName = FirstName, LastName = LastName, Level = Level, Since = Since};
                        return member;
                    }
                    catch { }
                }
                */

                //Alternative 1: A better alternative and perhaps clearer alternative
                var Level = (MemberLevel)rnd.Next((int)MemberLevel.Platinum, (int)MemberLevel.Blue + 1);

                var _fnames = "Harry, Lord, Hermione, Albus, Severus, Ron, Draco, Frodo, Gandalf, Sam, Peregrin, Saruman".Split(", ");
                var _lnames = "Potter, Voldemort, Granger, Dumbledore, Snape, Malfoy, Baggins, the Gray, Gamgee, Took, the White".Split(", ");
                var FirstName = _fnames[rnd.Next(0, _fnames.Length)];
                var LastName = _lnames[rnd.Next(0, _lnames.Length)];

                bool dateOK = false;
                DateTime Since = default;
                while (!dateOK)
                {
                    try
                    {
                        int year = rnd.Next(1980, DateTime.Today.Year + 1);
                        int month = rnd.Next(1, 13);
                        int day = rnd.Next(1, 31);

                        Since = new DateTime(year, month, day);
                        dateOK = true;
                      }
                    catch 
                    {
                        dateOK = true;
                    }
                }
                var member = new Member { FirstName = FirstName, LastName = LastName, Level = Level, Since = Since };
                return member;
            }
        }
        #endregion

        public Member() { }

        #region Copy Constructor
        public Member(IMember src)
        {
            FirstName = src.FirstName;
            LastName = src.LastName;
            Level = src.Level;
            Since = src.Since;
        }
        #endregion
    }
}
