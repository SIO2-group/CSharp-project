﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fulbank.classes;
using Fulbank;
using MySql.Data.MySqlClient;
using FulBank;

namespace Fulbank.pages
{
    public partial class FormTransfer : Form
    {
        private const int CB_SETCUEBANNER = 0x1703;
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lParam);
        private User user;
        Account anAccountFrom;
        Account anAccountTo;
        List<Panel> panelsAccounts = new List<Panel>();


        public FormTransfer(User AUser)
        {
            InitializeComponent();
           user = AUser;
            this.Text = String.Empty;
            this.ControlBox = false;
        }

        private void FormTransfer_Load(object sender, EventArgs e)
        {
            SendMessage(this.ComboAccountsFrom.Handle, CB_SETCUEBANNER, 0, "Depuis le compte");
            SendMessage(this.ComboAccountsTo.Handle, CB_SETCUEBANNER, 0, "Vers le compte");
            ComboAccountsFrom.Items.Clear();
            ComboAccountsTo.Items.Clear();
            foreach(Account account in user.GetAccounts())
            {
                ComboAccountsFrom.Items.Add(account.Get_AccountType().Get_Label());
            }
            foreach (Account account in user.GetAccounts())
            {
                ComboAccountsTo.Items.Add(account.Get_AccountType().Get_Label());
            }
            foreach (Beneficiary beneficiary in user.GetBeneficiary())
            {
                ComboAccountsTo.Items.Add(beneficiary.getBeneficiaryName());
            }
        }

        private void ComboAccountsFrom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboAccountsTo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ButtonAddBeneficiary_Click(object sender, EventArgs e)
        {
            new FormAddBeneficiary(user).Show();
        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(TransferValue.Text) && !String.IsNullOrEmpty(ComboAccountsFrom.SelectedItem.ToString()) && !String.IsNullOrEmpty(ComboAccountsTo.SelectedItem.ToString()))
            {
                if(double.Parse(TransferValue.Text) > 0)
                {
                    if(!(ComboAccountsTo.Text == ComboAccountsFrom.Text))
                    {
                        foreach(Account account in user.GetAccounts())
                        {
                            if (account.Get_AccountType().Get_Label() == ComboAccountsFrom.SelectedItem.ToString())
                            {
                                anAccountFrom = account;
                            }
                        }
                  /*      if ((anAccountFrom.Get_Balance - ))
                        {

                        }
                        else
                        {
                            MessageBox.Show("Votre limite de découvert sera dépassée");
                        } */
                    }
                    else
                    {
                        MessageBox.Show("Veuillez sélectionner des comptes différents");
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez enter une valeur positive");
                }
                
            }
            else
            {
                if (!String.IsNullOrEmpty(TransferValue.Text)){
                    MessageBox.Show("Veuillez entrer une valeur de virement");
                    if (!String.IsNullOrEmpty(ComboAccountsFrom.Text))
                    {
                        MessageBox.Show("Veuillez sélectionner un compte d'envoi");
                        if (!String.IsNullOrEmpty(ComboAccountsTo.Text))
                        {
                            MessageBox.Show("Veuillez sélectionner un compte de réception");
                        }
                    }
                }
                
            }
        }
    }
}