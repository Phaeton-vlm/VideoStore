using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore
{
    public class AppUpdate
    {
        public delegate void Authorize();
        public delegate void CashChangeHandler(double x);

        static event Authorize updateApp;
        public static event Authorize UpdateApp
        {
            add
            {
                updateApp += value;
            }
            remove
            {
                showLibrary = null;
            }
        }



        public static event CashChangeHandler ChangeCash;
        public static event Authorize ShowLoginDialog;

        static event Authorize checkButton;
        public static event Authorize CheckButton
        {
            add
            {
                checkButton = null;
                checkButton += value;
            }
            remove
            {
                checkButton = null;
            }
        }

        static event Authorize checkServices;
        public static event Authorize CheckServices
        {
            add
            {
                checkServices = null;
                checkServices += value;
            }
            remove
            {
                checkServices = null;
            }
        }
        public static bool GoLibrary { get; set; } = false;

        static event Authorize showLibrary;
        public static event Authorize ShowLibrary
        {
            add
            {
                if(showLibrary == null)
                {
                    showLibrary += value;
                }
            }
            remove
            {
                showLibrary = null;
            }
        }

        static event Authorize updateCash;
        public static event Authorize UpdateCash
        {
            add
            {
                updateCash = null;
                updateCash += value;
            }
            remove
            {
                updateCash = null;
            }
        }


        public AppUpdate()
        {

        }

        public static void Update()
        {
            updateApp?.Invoke();
            ChangeButton();
            GoLib();
            UpdateServices();
        }

        public static void ChangeButton()
        {
            checkButton?.Invoke();
        }

        public static void GoLib()
        {
            if (GoLibrary)
            {
                showLibrary?.Invoke();
                GoLibrary = false;
            }
        }

        public static void LogonDialogOpen()
        {
            ShowLoginDialog?.Invoke();
        }

        public static void ToBuyMovie(double price)
        {
            CurrentUser.CardBalance -= price;
            ChangeCash?.Invoke(CurrentUser.CardBalance);
        }

        public static void UpdateServices()
        {
            checkServices?.Invoke();
        }

        public static void UpdateCashValue()
        {
            updateCash?.Invoke();
        }
    }
}
