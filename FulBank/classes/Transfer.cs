﻿using FulBank;
using MySql.Data.MySqlClient;
using System;


namespace Fulbank.classes
{
    public class Transfer
    {
        private double _amount;
        private Account _accountFrom;
        private Account _accountTo;
        private Beneficiary _beneficiary;
        private DateTime _date;

        public Transfer(double amount, DateTime date, Account accountFrom, Account accountTo)
        {
            _amount = amount;
            _accountFrom = accountFrom;
            _accountTo = accountTo;
            _date = date;
            _beneficiary = new Beneficiary();
        }

        public Transfer(double amount, DateTime date, Account accountFrom, Account accountTo, Beneficiary aBeneficiary)
        {
            _amount = amount;
            _accountFrom = accountFrom;
            _accountTo = accountTo;
            _beneficiary = aBeneficiary;
            _date = date;
        }


        internal void sendToAccount()
        {
            FormMain.dbConnexion.Open();
            string commandTextTerminalId = "SELECT TL_ID FROM terminal WHERE TL_BUILDING = '"+ FormMain.thisTerminal.getBuilding() + "' AND TL_CITY = '" + FormMain.thisTerminal.getCity() + "' AND TL_IP = '" + FormMain.thisTerminal.getIp() + "'";
            MySqlCommand cmdGetTerminalId = new MySqlCommand(commandTextTerminalId, FormMain.dbConnexion);
            int terminalId = int.Parse(cmdGetTerminalId.ExecuteScalar().ToString());

            string commandTextTransferSend = "INSERT INTO transaction(T_ID_ACCOUNT_TO, T_ID_ACCOUNT_FROM, T_AMOUNT, T_DATE, T_TL_ID) VALUES('" + _accountTo.Get_Id() + "', '" + _accountFrom.Get_Id() + "','" + _amount + "', '" + _date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "', '" + terminalId + "' )";
            MySqlCommand cmdGetUserAccounts = new MySqlCommand(commandTextTransferSend, FormMain.dbConnexion);
            cmdGetUserAccounts.ExecuteNonQuery();

            Console.Write(commandTextTransferSend);

            FormMain.dbConnexion.Close();
        }

        internal void sendToBeneficiary()
        {
            FormMain.dbConnexion.Open();
            string commandTextTerminalId = "SELECT TL_ID FROM terminal WHERE TL_BUILDING = '" + FormMain.thisTerminal.getBuilding() + "' AND TL_CITY = '" + FormMain.thisTerminal.getCity() + "' AND TL_IP = '" + FormMain.thisTerminal.getIp() + "'";
            MySqlCommand cmdGetTerminalId = new MySqlCommand(commandTextTerminalId, FormMain.dbConnexion);

            int terminalId = int.Parse(cmdGetTerminalId.ExecuteScalar().ToString());

            string commandTextTransferSend = "INSERT INTO transaction(T_ID_ACCOUNT_TO, T_ID_ACCOUNT_FROM, T_AMOUNT, T_DATE, T_TL_ID) VALUES('" + _beneficiary.getBeneficiaryId() + "', '" + _accountFrom.Get_Id() + "','" + _amount + "', '" + _date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "', '" + terminalId + "' )";
            MySqlCommand cmdGetUserAccounts = new MySqlCommand(commandTextTransferSend, FormMain.dbConnexion);
            cmdGetUserAccounts.ExecuteNonQuery();

            Console.Write(commandTextTransferSend);

            FormMain.dbConnexion.Close();
        }
   
        public double getAmount()
        {
            return _amount;
        }

        public Account getAccountFrom()
        {
            return _accountFrom;
        }
        public Account getAccountTo()
        {
            return _accountTo;
        }
        public Beneficiary getBeneficiaryTo()
        {
            return _beneficiary;
        }

        public DateTime getDate()
        {
            return _date;
        }

    }

}
