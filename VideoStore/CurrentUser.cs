using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore
{
    public static class CurrentUser
    {
        public static bool Authorized { get; set; }
        public static int UserID { get; set; }
        public static string UserName { get; set; }
        public static string UserSurname { get; set; }
        public static string UserMiddlename { get; set; }
        public static string CardNumber { get; set; }
        public static double CardBalance { get; set; }
        public static DateTime RegisterDate { get; set; }
        static CurrentUser()
        {
            Authorized = false;
        }

        public static void SetUserInfo(int _userID,string _userName, string _userSurname, string _userMiddleName, double _cardBalance, string _cardNumber, DateTime _registerDate)
        {
            Authorized = true;
            UserID = _userID;
            UserName = _userName;
            UserSurname = _userSurname;
            UserMiddlename = _userMiddleName;
            CardNumber = _cardNumber;
            CardBalance = _cardBalance;
            RegisterDate = _registerDate;
        }

        public static void ResetUser()
        {
            Authorized = false;
            UserID = 0;
            UserName = "";
            UserSurname = "";
            UserMiddlename = "";
            CardNumber = "";
            CardBalance = 0;
        }

        public static bool CanBuy(double price)
        {
            if(CardBalance - price >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
