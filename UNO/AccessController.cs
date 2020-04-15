using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace UNO
{
    public class AccessController
    {
        public static string AppMode = "";
        private string _Controller_Id;
        private string _Controller_Desc;
        private string _Controller_Type;
        private string _Controller_Ip;
        private string _Subnet_Mask;
        private string _Getway_Id;
        private string _Firmware_Rev_No;
        private string _Hardware_Ver_No;
        private string _Antipassback;
        private string _AuthMode;
        private int _Incoming_Port;
        private int _Outgoing_Port;
        private int _Facility_cd1;
        private int _Facility_cd2;
        private int _Facility_cd3;
        private int _Facility_cd4;
        private int _Facility_cd5;
        private int _Facility_cd6;
        private int _Changed_by;
        private char _Changed_Type;
        private DateTime _Changed_when;
        public static string m_connecton = ConfigurationManager.ConnectionStrings["connection_string"].ToString();

        public string ControllerId
        {
            get { return _Controller_Id; }
            set { _Controller_Id = value; }
        }
        public string ControllerDesc
        {
            get { return _Controller_Desc; }
            set { _Controller_Desc = value; }
        }
        public string ControllerType
        {
            get { return _Controller_Type; }
            set { _Controller_Type = value; }
        }
        public string ControllerIp
        {
            get { return _Controller_Ip; }
            set { _Controller_Ip = value; }
        }
        public string SubnetMask
        {
            get { return _Subnet_Mask; }
            set { _Subnet_Mask = value; }
        }
        public string GetwayId
        {
            get { return _Getway_Id; }
            set { _Getway_Id = value; }
        }
        public string FirmwareRevNo
        {
            get { return _Firmware_Rev_No; }
            set { _Firmware_Rev_No = value; }
        }
        public string HardwareVerNo
        {
            get { return _Hardware_Ver_No; }
            set { _Hardware_Ver_No = value; }
        }
        public string Antipassback
        {
            get { return _Antipassback; }
            set { _Antipassback = value; }
        }
        public string AuthMode
        {
            get { return _AuthMode; }
            set { _AuthMode = value; }
        }
        public int IncomingPort
        {
            get { return _Incoming_Port; }
            set { _Incoming_Port = value; }
        }
        public int OutgoingPort
        {
            get { return _Outgoing_Port; }
            set { _Outgoing_Port = value; }
        }
        public int Facilitycd1
        {
            get { return _Facility_cd1; }
            set { _Facility_cd1 = value; }
        }
        public int Facility_cd2
        {
            get { return _Facility_cd2; }
            set { _Facility_cd2 = value; }
        }
        public int Facility_cd3
        {
            get { return _Facility_cd3; }
            set { _Facility_cd3 = value; }
        }
        public int Facility_cd4
        {
            get { return _Facility_cd4; }
            set { _Facility_cd4 = value; }
        }
        public int Facility_cd5
        {
            get { return _Facility_cd5; }
            set { _Facility_cd5 = value; }
        }
        public int Facility_cd6
        {
            get { return _Facility_cd6; }
            set { _Facility_cd6 = value; }
        }
        public DateTime Changed_when
        {
            get { return _Changed_when; }
            set { _Changed_when = value; }
        }
        public int Changed_by
        {
            get { return _Changed_by; }
            set { _Changed_by = value; }
        }
        public char Changed_Type
        {
            get { return _Changed_Type; }
            set { _Changed_Type = value; }
        }
    }
}