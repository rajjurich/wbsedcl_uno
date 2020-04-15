using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

namespace UNO
{
    public class clsCardRW
    {
        public ContactlessCardRW.CardClass obj_Card = new ContactlessCardRW.CardClass();
        //public ReadCard.clsReadCard objReadCard = new ReadCard.clsReadCard();
        public static string strMasterKey;
        public int NativeSector = 16;
        public int ISOSector = 35;
        public string MasterUId = "";
        public string MasterUPass = "";
        public static int FingerQty = 60;
        public static int NoOfRetries = 2;
        public static int TimeOut = 20;
        public static string finger_val1 = "TempMin4";                            //Stores the number of first finger to Enrolled.
        public static string finger_val2 = "TempMin7";
        public static string ISOfinger_val1 = "ISOTempMin4";                            //Stores the number of first finger to Enrolled.
        public static string ISOfinger_val2 = "ISOTempMin7";
        int fingerIndex1;
        int fingerIndex2;
        public static bool Execute = false;

        public string GetSector(int nBlock)
        {
            string functionReturnValue = null;
            switch ((nBlock))
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    functionReturnValue = "00";
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                    functionReturnValue = "01";
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                    functionReturnValue = "02";
                    break;
                case 12:
                case 13:
                case 14:
                case 15:

                    functionReturnValue = "03";
                    break;
                case 16:
                case 17:
                case 18:
                case 19:

                    functionReturnValue = "04";
                    break;
                case 20:
                case 21:
                case 22:
                case 23:

                    functionReturnValue = "05";
                    break;
                case 24:
                case 25:
                case 26:
                case 27:

                    functionReturnValue = "06";
                    break;
                case 28:
                case 29:
                case 30:
                case 31:

                    functionReturnValue = "07";
                    break;
                case 32:
                case 33:
                case 34:
                case 35:

                    functionReturnValue = "08";
                    break;
                case 36:
                case 37:
                case 38:
                case 39:
                    functionReturnValue = "09";
                    break;
                case 40:
                case 41:
                case 42:
                case 43:
                    functionReturnValue = "10";
                    break;
                case 44:
                case 45:
                case 46:
                case 47:
                    functionReturnValue = "11";
                    break;
                case 48:
                case 49:
                case 50:
                case 51:
                    functionReturnValue = "12";
                    break;
                case 52:
                case 53:
                case 54:
                case 55:
                    functionReturnValue = "13";
                    break;
                case 56:
                case 57:
                case 58:
                case 59:

                    functionReturnValue = "14";
                    break;
                case 60:
                case 61:
                case 62:
                case 63:
                    functionReturnValue = "15";
                    break;
                case 64:
                case 65:
                case 66:
                case 67:
                    functionReturnValue = "16";
                    break;
                case 68:
                case 69:
                case 70:
                case 71:
                    functionReturnValue = "17";
                    break;
                case 72:
                case 73:
                case 74:
                case 75:
                    functionReturnValue = "18";
                    break;
                case 76:
                case 77:
                case 78:
                case 79:
                    functionReturnValue = "19";
                    break;
                case 80:
                case 81:
                case 82:
                case 83:
                    functionReturnValue = "20";
                    break;
                case 84:
                case 85:
                case 86:
                case 87:
                    functionReturnValue = "21";
                    break;
                case 88:
                case 89:
                case 90:
                case 91:
                    functionReturnValue = "22";
                    break;
                case 92:
                case 93:
                case 94:
                case 95:
                    functionReturnValue = "23";
                    break;
                case 96:
                case 97:
                case 98:
                case 99:
                    functionReturnValue = "24";
                    break;
                case 100:
                case 101:
                case 102:
                case 103:
                    functionReturnValue = "25";
                    break;
                case 104:
                case 105:
                case 106:
                case 107:
                    functionReturnValue = "26";
                    break;
                case 108:
                case 109:
                case 110:
                case 111:
                    functionReturnValue = "27";
                    break;
                case 112:
                case 113:
                case 114:
                case 115:
                    functionReturnValue = "28";
                    break;
                case 116:
                case 117:
                case 118:
                case 119:
                    functionReturnValue = "29";
                    break;
                case 120:
                case 121:
                case 122:
                case 123:
                    functionReturnValue = "30";
                    break;
                case 124:
                case 125:
                case 126:
                case 127:
                    functionReturnValue = "31";
                    break;
                case 128:
                case 129:
                case 130:
                case 131:
                case 132:
                case 133:
                case 134:
                case 135:
                case 136:
                case 137:
                case 138:
                case 139:
                case 140:
                case 141:
                case 142:
                case 143:
                    functionReturnValue = "32";
                    break;                   
                case 144:
                case 145:
                case 146:
                case 147:
                case 148:
                case 149:
                case 150:
                case 151:
                case 152:
                case 153:
                case 154:
                case 155:
                case 156:
                case 157:
                case 158:
                case 159:
                    functionReturnValue = "33";
                    break;                
                case 160:
                case 161:
                case 162:
                case 163:
                case 164:
                case 165:
                case 166:
                case 167:
                case 168:
                case 169:
                case 170:
                case 171:
                case 172:
                case 173:
                case 174:
                case 175:
                    functionReturnValue = "34";
                    break;                   
                case 176:
                case 177:
                case 178:
                case 179:
                case 180:
                case 181:
                case 182:
                case 183:
                case 184:
                case 185:
                case 186:
                case 187:
                case 188:
                case 189:
                case 190:
                case 191:
                    functionReturnValue = "35";
                    break;                    
                case 192:
                case 193:
                case 194:
                case 195:
                case 196:
                case 197:
                case 198:
                case 199:
                case 200:
                case 201:
                case 202:
                case 203:
                case 204:
                case 205:
                case 206:
                case 207:
                    functionReturnValue = "36";
                    break;                    
                case 208:
                case 209:
                case 210:
                case 211:
                case 212:
                case 213:
                case 214:
                case 215:
                case 216:
                case 217:
                case 218:
                case 219:
                case 220:
                case 221:
                case 222:
                case 223:
                    functionReturnValue = "37";
                    break;                   
                case 224:
                case 225:
                case 226:
                case 227:
                case 228:
                case 229:
                case 230:
                case 231:
                case 232:
                case 233:
                case 234:
                case 235:
                case 240:
                case 241:
                case 242:
                case 243:
                    functionReturnValue = "38";
                    break;                
                case 236:
                case 237:
                case 238:
                case 239:
                case 244:
                case 245:
                case 246:
                case 247:
                case 248:
                case 249:
                case 250:
                case 251:
                case 252:
                case 253:
                case 254:
                case 255:
                    functionReturnValue = "39";
                    break;                   
            }
            return functionReturnValue;
        }

        public string WriteCard(byte[] finger1, byte[] finger2,int fingerQty1,int fingerQty2)
        {
            try
            {
                String ErrorMessage = "";
                String secno;
                int intsc;
                int BIRLength = 0;

                //string DataIntial = "";
                //DataIntial = obj_Card.Initialise().Trim();
                //if (DataIntial != "")
                //    return "Omnikey reader not connected or Error in card Initialization";

                string Data = "";
                //Data = obj_Card.ConnectToCard();
                //if (Data != "")
                //    return "Error in connecting";

                //if (strMasterKey == null)
                //{
                //    //place Master card on reader                             

                //    obj_Card.userid = "111111";
                //    obj_Card.pwd = "111111";
                //    strMasterKey = obj_Card.ReadMasterKey(obj_Card.userid, obj_Card.pwd);
                //    if (strMasterKey.Contains(" "))
                //    {
                //        string ErrorMsg = strMasterKey;
                //        strMasterKey = null;
                //        return ErrorMsg;
                //    }
                //    //Place User Card on Reader
                //}

                if (strMasterKey == null)
                {
                    strMasterKey = "FFFFFFFFFFFF";
                }

                Data = obj_Card.Initialise().Trim();
                if (Data != "")
                    return "Error in Initialization";

                Data = "";
                Data = obj_Card.ConnectToCard();
                if (Data != "")
                    return "Error in connecting";

                Data = obj_Card.LoadKey(strMasterKey).Trim();
                if (Data != "")
                    return "Error in Load Key";


                for (int cnt = 16; cnt > 20; cnt++)
                {
                    string pcard;
                    pcard = RawCard(cnt);

                    if (pcard != "")
                    {
                        return pcard;
                    }
                }

                string pcard1;
                pcard1 = ISORawCard();

                if (pcard1 != "")
                {
                    return pcard1;
                }

                String LSTransKey;
                String AccessKey;

                AccessKey = "FFFFFFFFFFFF";

                int x = 0;
                int xy = 0;
                //  int pos = 0;
                CardSettings();
                intsc = NativeSector;

                int a1 = intsc * 4;
                int a2 = (intsc * 4) + 1;
                int a3 = (intsc * 4) + 2;
                int a4 = (intsc * 4) + 3;
                int a5 = (intsc * 4) + 4;
                int a6 = (intsc * 4) + 5;
                int a7 = (intsc * 4) + 6;
                int a8 = (intsc * 4) + 7;
                int a9 = (intsc * 4) + 8;
                int a10 = (intsc * 4) + 9;
                int a11 = (intsc * 4) + 10;
                int a12 = (intsc * 4) + 11;
                int a13 = (intsc * 4) + 12;
                int a14 = (intsc * 4) + 13;
                int a15 = (intsc * 4) + 14;
                int a16 = (intsc * 4) + 15;
                int a17 = a16 + 1;
                int a18 = a17 + 1;
                int a19 = a18 + 1;
                int a20 = a19 + 1;
                int a21 = a20 + 1;
                int a22 = a21 + 1;
                int a23 = a22 + 1;
                int a24 = a23 + 1;
                int a25 = a24 + 1;
                int a26 = a25 + 1;
                int a27 = a26 + 1;
                int a28 = a27 + 1;
                int a29 = a28 + 1;
                int a30 = a29 + 1;
                int a31 = a30 + 1;
                int a32 = a31 + 1;
                int a33 = a32 + 1;
                int a34 = a33 + 1;
                int a35 = a34 + 1;
                int a36 = a35 + 1;
                int a37 = a36 + 1;
                int a38 = a37 + 1;
                int a39 = a38 + 1;
                int a40 = a39 + 1;
                int a41 = a40 + 1;
                int a42 = a41 + 1;
                int a43 = a42 + 1;
                int a44 = a43 + 1;
                int a45 = a44 + 1;
                int a46 = a45 + 1;
                int a47 = a46 + 1;
                int a48 = a47 + 1;

                int nBlock = (intsc * 4);

                //********* According to BIR Storage format excluding 2 finger minutiae data size,constatnt Length is 22 **********
                BIRLength = 534;
                //BIRLength += finger1.Length;
                //BIRLength += finger2.Length;
                //*********end                       

                int AllData = 0;
                int d1 = 0;
                string AllData1 = "";
                int intsleep = 500;

                for (x = 0; x <= finger1.Length; x++)
                {
                    String sdata = "";
                    String FormatIdentifier = "464D5200";
                    String VersionNo = "312E3000";
                    String BioDataFormat = "0101";
                    int TotalQty = (fingerQty1 + fingerQty2) / 2;
                    String OverallQuality = Bin.Converse.IntToHex(TotalQty, 1).ToString();
                    String NoofFingers = "02";
                    String LenOfBIR = Bin.Converse.IntToHex(BIRLength, 1).ToString();
                    String Finger1No = Bin.Converse.IntToHex(fingerIndex1, 1).ToString();
                    String Finger1Qty = Bin.Converse.IntToHex(fingerQty1, 1).ToString();

                    if (nBlock == a1)
                    {
                        secno = GetSector((int)(a1));
                        //d1 = Bin.Converse.HexToInt("43");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("4D");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("53");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("43");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("4F");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("4D");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("50");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("55");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("54");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("45");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("52");
                        //AllData = AllData + d1;
                        //d1 = Bin.Converse.HexToInt("53");
                        //AllData = AllData + d1;                  


                        sdata = FormatIdentifier + VersionNo + BioDataFormat + OverallQuality + NoofFingers + LenOfBIR;
                        //sdata += LenOfBIR + Finger1No + Finger1Qty;

                        //d1 = finger1.Length;
                        //AllData = AllData + d1;

                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), strMasterKey, 97);
                            if (Data != "")
                            {
                                Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), AccessKey, 96);
                                if (Data != "")
                                {
                                    System.Threading.Thread.Sleep(intsleep);
                                    Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), AccessKey, 97);
                                    if (Data != "")
                                        return "Authentication Error in Block " + a1;
                                }
                            }
                        }
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a1.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a1;
                    }
                    else if (nBlock == a2)
                    {
                        sdata = "";
                        sdata = Finger1No + Finger1Qty + Bin.Converse.IntToHex(finger1.Length, 2).ToString();

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a2.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a2;


                    }
                    else if (nBlock == a3)
                    {
                        sdata = "";
                        for (x = 0; x <= 15; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString();
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a3.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a3;

                    }
                    //Key Block a4
                    else if (nBlock == a4)
                    {
                        //System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a4.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a4.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a4;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a4.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a4;
                    }
                    else if (nBlock == a5)
                    {
                        secno = GetSector((int)(a5));
                        sdata = "";
                        for (x = 16; x <= 31; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString();
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }
                        //System.Windows.Forms.Application.DoEvents();

                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a5.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a5.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a5;
                        }
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a5.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a5;
                    }
                    else if (nBlock == a6)
                    {
                        sdata = "";
                        for (x = 32; x <= 47; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString();
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a6.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a6;
                    }
                    else if (nBlock == a7)
                    {
                        sdata = "";
                        if (finger1.Length > 63)
                        {
                            xy = 63;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 48; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a7.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a7;
                    }
                    //Key Block a8
                    else if (nBlock == a8)
                    {
                        //System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a8.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a8.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a8;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a8.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a8;
                    }


                    else if (nBlock == a9)
                    {
                        secno = GetSector((int)(a9));
                        sdata = "";
                        if (finger1.Length > 79)
                        {
                            xy = 79;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 64; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a9.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a9.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a9;
                        }
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a9.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a9;

                    }
                    else if (nBlock == a10)
                    {

                        sdata = "";
                        if (finger1.Length > 95)
                        {
                            xy = 95;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 80; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a10.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a10;
                    }

                    else if (nBlock == a11)
                    {
                        sdata = "";
                        if (finger1.Length > 111)
                        {
                            xy = 111;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }

                        for (x = 96; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a11.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a11;
                    }
                    //Key Block
                    else if (nBlock == a12)
                    {
                        //System.Windows.Forms.Application.DoEvents();
                        Data = obj_Card.Authenticate(Convert.ToInt32(a12.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            Data = obj_Card.Authenticate(Convert.ToInt32(a12.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a12;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a12.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a12;
                    }
                    else if (nBlock == a13)
                    {
                        secno = GetSector((int)(a13));
                        sdata = "";
                        if (finger1.Length > 127)
                        {
                            xy = 127;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 112; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.Authenticate(Convert.ToInt32(a13.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a13.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a13;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a13.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a13;

                    }
                    else if (nBlock == a14)
                    {
                        sdata = "";
                        if (finger1.Length > 143)
                        {
                            xy = 143;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 128; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        //System.Windows.Forms.Application.DoEvents();
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a14.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a14;
                    }

                    else if (nBlock == a15)
                    {
                        sdata = "";
                        if (finger1.Length > 159)
                        {
                            xy = 159;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 144; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a15.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a15;
                    }
                    //Key Block
                    else if (nBlock == a16)
                    {
                        //System.Windows.Forms.Application.DoEvents();
                        Data = obj_Card.Authenticate(Convert.ToInt32(a16.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            Data = obj_Card.Authenticate(Convert.ToInt32(a16.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a16;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a16.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a16;
                    }

                    else if (nBlock == a17)
                    {
                        secno = GetSector((int)(a17));
                        sdata = "";
                        if (finger1.Length > 175)
                        {
                            xy = 175;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 160; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }
                        Data = obj_Card.Authenticate(Convert.ToInt32(a17.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a17.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a17;
                        }
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a17.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a17;
                    }
                    ////}
                    //nBlock++;
                    else if (nBlock == a18)
                    {
                        sdata = "";
                        if (finger1.Length > 191)
                        {
                            xy = 191;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 176; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        // System.Windows.Forms.Application.DoEvents();

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a18.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a18;
                    }
                    else if (nBlock == a19)
                    {
                        sdata = "";
                        if (finger1.Length > 207)
                        {
                            xy = 207;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 192; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a19.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a19;
                    }
                    //Key Block
                    else if (nBlock == a20)
                    {
                        if (Data != "")
                        {
                            Data = obj_Card.Authenticate(Convert.ToInt32(a20.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a20;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a20.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a20;
                    }

                    else if (nBlock == a21)
                    {
                        secno = GetSector((int)(a21));
                        sdata = "";
                        if (finger1.Length > 223)
                        {
                            xy = 223;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 208; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }
                        Data = obj_Card.Authenticate(Convert.ToInt32(a21.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a21.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a21;
                        }
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a21.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a21;
                    }

                    else if (nBlock == a22)
                    {
                        sdata = "";
                        if (finger1.Length > 239)
                        {
                            xy = 239;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 224; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        //System.Windows.Forms.Application.DoEvents();
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a22.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a22;
                    }
                    else if (nBlock == a23)
                    {
                        sdata = "";
                        if (finger1.Length > 255)
                        {
                            xy = 255;
                        }
                        else
                        {
                            xy = finger1.Length - 1;
                        }
                        for (x = 240; x <= xy; x++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger1[x], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger1[x]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a23.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a23;
                    }
                    //Key Block
                    else if (nBlock == a24)
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a24.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            Data = obj_Card.Authenticate(Convert.ToInt32(a24.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a24;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a24.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a24;
                    }
                    nBlock++;

                }
                nBlock = a25;
                int y = 0;
                String Finger2No = Bin.Converse.IntToHex(fingerIndex2, 1).ToString();
                String Finger2Qty = Bin.Converse.IntToHex(fingerQty2, 1).ToString();
                for (y = 0; y <= finger2.Length + 1; y++)
                {
                    String sdata;

                    if (nBlock == a25)
                    {
                        secno = GetSector((int)(a25));

                        sdata = "";
                        sdata = Finger2No + Finger2Qty + Bin.Converse.IntToHex(finger2.Length, 2).ToString();

                        //d1 = finger2.Length;
                        //AllData = AllData + d1;

                        // System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a25.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a25.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a25;
                        }
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a25.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a25;
                    }

                    else if (nBlock == a26)
                    {
                        sdata = "";
                        for (y = 0; y <= 15; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a26.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a26;

                    }
                    else if (nBlock == a27)
                    {
                        sdata = "";
                        for (y = 16; y <= 31; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a27.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a27;

                    }
                    //Key Block
                    else if (nBlock == a28)
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a28.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            Data = obj_Card.Authenticate(Convert.ToInt32(a28.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a28;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a28.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a28;
                    }

                    else if (nBlock == a29)
                    {
                        secno = GetSector((int)(a29));
                        sdata = "";
                        for (y = 32; y <= 47; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        // System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a29.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a29.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a29;
                        }
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a29.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a29;
                    }

                    else if (nBlock == a30)
                    {
                        sdata = "";
                        for (y = 48; y <= 63; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }
                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //if (sdata.Length == 30)
                        //{
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), sdata + AllData1);
                        //}
                        //else
                        //{
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a30;
                    }
                    else if (nBlock == a31)
                    {
                        sdata = "";

                        if (finger2.Length > 79)
                        {
                            xy = 79;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 64; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //if (sdata.Length == 30)
                        //{
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        //objKCard.mifsw_write_card(a23.ToString(), sdata, out llng1);
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), sdata.PadRight(32, '0'));
                        //}

                        if (Data != "")
                            return "Error in Writing in Block " + a31;
                    }

                    // Key Block
                    else if (nBlock == a32)
                    {
                        //if (AllData1.Length != 8)
                        //{
                        //    y = y - 1;
                        //}
                        Data = obj_Card.Authenticate(Convert.ToInt32(a32.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            Data = obj_Card.Authenticate(Convert.ToInt32(a32.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a32;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a32.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a32;

                    }

                    else if (nBlock == a33)
                    {
                        secno = GetSector((int)(a33));
                        sdata = "";

                        if (finger2.Length > 95)
                        {
                            xy = 95;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 80; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a33;
                        }
                        //if (sdata.Length == 30)
                        //{
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    //objKCard.mifsw_write_card(a25.ToString(), sdata + AllData1, out llng1);
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a33;
                    }

                    else if (nBlock == a34)
                    {
                        sdata = "";

                        if (finger2.Length > 111)
                        {
                            xy = 111;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 96; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //if (sdata.Length == 30)
                        //{
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a34;
                    }

                    else if (nBlock == a35)
                    {
                        sdata = "";

                        if (finger2.Length > 127)
                        {
                            xy = 127;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 112; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //if (sdata.Length == 30)
                        //{
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a35;
                    }

                    //Key Block
                    else if (nBlock == a36)
                    {
                        //if (AllData1.Length != 8)
                        //{
                        //    y = y - 1;
                        //}

                        //System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a36.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a36.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a36;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a36.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a36;
                    }
                    else if (nBlock == a37)
                    {
                        secno = GetSector((int)(a37));

                        sdata = "";

                        if (finger2.Length > 143)
                        {
                            xy = 143;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 128; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //sdata = sdata + System.Convert.ToChar(finger2[y]).ToString();
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a37.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a37.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a37;
                        }
                        //if (sdata.Length == 30)
                        //{
                        //    //objKCard.mifsw_write_card(a29.ToString(), sdata + AllData1.Substring(0, 2), out llng1);
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    //objKCard.mifsw_write_card(a29.ToString(), sdata + AllData1.Substring(0, 4), out llng1);
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    //objKCard.mifsw_write_card(a29.ToString(), sdata + AllData1.Substring(0, 6), out llng1);
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    //objKCard.mifsw_write_card(a29.ToString(), AllData1, out llng1);
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    //objKCard.mifsw_write_card(a29.ToString(), sdata + AllData1, out llng1);
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        //objKCard.mifsw_write_card(a29.ToString(), sdata, out llng1);
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a37;

                    }

                    else if (nBlock == a38)
                    {
                        sdata = "";

                        if (finger2.Length > 159)
                        {
                            xy = 159;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 144; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //if (sdata.Length == 30)
                        //{
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a38.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    //objKCard.mifsw_write_card(a30.ToString(), sdata + AllData1.Substring(0, 4), out llng1);
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a38.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    //objKCard.mifsw_write_card(a30.ToString(), sdata + AllData1.Substring(0, 6), out llng1);
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a38.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    //objKCard.mifsw_write_card(a30.ToString(), AllData1, out llng1);
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a38.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    //objKCard.mifsw_write_card(a30.ToString(), sdata + AllData1, out llng1);
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a38.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        //objKCard.mifsw_write_card(a30.ToString(), sdata, out llng1);
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a38.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a38;
                    }

                    else if (nBlock == a39)
                    {
                        //sdata = "";
                        //for (y = 160; y <= finger1.Length - 1; y++)
                        //{
                        //    sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                        //    d1 = Convert.ToInt16(finger2[y]);
                        //    AllData = AllData + d1;
                        //}

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), sdata.PadRight(32,'0'));
                        //if (Data != "")
                        //    return "Error in Writing in Block " + a39;

                        sdata = "";

                        if (finger2.Length > 175)
                        {
                            xy = 175;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 160; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //if (sdata.Length == 30)
                        //{
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a39;

                    }
                    // Key Block
                    else if (nBlock == a40)
                    {
                        //if (AllData1.Length != 8)
                        //{
                        //    y = y - 1;
                        //}
                        //System.Windows.Forms.Application.DoEvents();
                        Data = obj_Card.Authenticate(Convert.ToInt32(a40.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a40.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a40;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a40.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a40;
                    }
                    else if (nBlock == a41)
                    {
                        secno = GetSector((int)(a41));
                        sdata = "";

                        if (finger2.Length > 191)
                        {
                            xy = 191;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 176; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                            d1 = Convert.ToInt16(finger2[y]);
                            AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a41.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a41.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a41;
                        }
                        //if (sdata.Length == 30)
                        //{
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a41.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a41.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a41.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a41.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    //objKCard.mifsw_write_card(a25.ToString(), sdata + AllData1, out llng1);
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a41.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a41.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a41;
                    }

                    else if (nBlock == a42)
                    {
                        sdata = "";

                        if (finger2.Length > 207)
                        {
                            xy = 207;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 192; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //if (sdata.Length == 30)
                        //{
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a42.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a42.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a42.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a42.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a42.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a42.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a42;
                    }
                    else if (nBlock == a43)
                    {
                        sdata = "";

                        if (finger2.Length > 223)
                        {
                            xy = 223;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 208; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //if (sdata.Length == 30)
                        //{
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a43.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a43.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a43.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a43.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a43.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a43.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a43;
                    }
                    //Key Block
                    else if (nBlock == a44)
                    {
                        //if (AllData1.Length != 8)
                        //{
                        //    y = y - 1;
                        //}
                        Data = obj_Card.Authenticate(Convert.ToInt32(a44.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            Data = obj_Card.Authenticate(Convert.ToInt32(a44.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a44;
                        }

                        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a44.ToString()), 1), strMasterKey, strMasterKey);
                        if (Data != "" && Data != null)
                            return "Error in Writing in Block " + a44;
                    }
                    else if (nBlock == a45)
                    {
                        secno = GetSector((int)(a45));

                        sdata = "";

                        if (finger2.Length > 239)
                        {
                            xy = 239;
                        }
                        else
                        {
                            xy = finger2.Length - 1;
                        }

                        for (y = 224; y <= xy; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        //System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a45.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a45.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a45;
                        }
                        //if (sdata.Length == 30)
                        //{
                        //    //objKCard.mifsw_write_card(a29.ToString(), sdata + AllData1.Substring(0, 2), out llng1);
                        //    string s30 = sdata + AllData1.Substring(0, 2);
                        //    s30 = s30.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a45.ToString()), s30);
                        //    AllData1 = AllData1.Substring(2);
                        //}
                        //else if (sdata.Length == 28)
                        //{
                        //    //objKCard.mifsw_write_card(a29.ToString(), sdata + AllData1.Substring(0, 4), out llng1);
                        //    string s28 = sdata + AllData1.Substring(0, 4);
                        //    s28 = s28.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a45.ToString()), s28);
                        //    AllData1 = AllData1.Substring(4);
                        //}
                        //else if (sdata.Length == 26)
                        //{
                        //    //objKCard.mifsw_write_card(a29.ToString(), sdata + AllData1.Substring(0, 6), out llng1);
                        //    string s26 = sdata + AllData1.Substring(0, 6);
                        //    s26 = s26.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a45.ToString()), s26);
                        //    AllData1 = AllData1.Substring(6);
                        //}
                        //else if (sdata.Length == 0)
                        //{
                        //    //objKCard.mifsw_write_card(a29.ToString(), AllData1, out llng1);
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a45.ToString()), AllData1.PadRight(32, '0'));
                        //    AllData1 = "";
                        //}
                        //else if (sdata.Length <= 24)
                        //{
                        //    //objKCard.mifsw_write_card(a29.ToString(), sdata + AllData1, out llng1);
                        //    string s24 = sdata + AllData1;
                        //    s24 = s24.PadRight(32, '0');
                        //    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a45.ToString()), s24);
                        //    AllData1 = "";
                        //}
                        //else
                        //{
                        //objKCard.mifsw_write_card(a29.ToString(), sdata, out llng1);
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a45.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a45;

                    }
                    else if (nBlock == a46)
                    {
                        sdata = "";
                        for (y = 240; y <= finger1.Length - 1; y++)
                        {
                            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                            //d1 = Convert.ToInt16(finger2[y]);
                            //AllData = AllData + d1;
                        }

                        //if (y == finger2.Length)
                        //{
                        //    AllData1 = Bin.Converse.IntToHex(AllData, 4);
                        //}

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a46.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a46;
                    }
                    else if (nBlock == a47)
                    {
                    }
                    //Key Block
                    else if (nBlock == a48)
                    {
                    }
                    nBlock++;
                }

                Data = obj_Card.DisConnectCard();
                if (Data != "")
                    return "Error in disconnecting";


                return ErrorMessage;
            }
            catch (Exception ex)
            {
                clsCardRW.Execute = false;
                return ex.Message;
            }
        }

        public string RawCard(int intsc)
        {
            String ErrorMessage = "";
            String secno;
            String TransKey;
            //int intsc = 18;            

            string Data = "";
            
            String LSTransKey;
            String AccessKey;

            AccessKey = "FFFFFFFFFFFF";
            
            //secno = "10";
           // intsc = 18;

            //TransKey = GetTransportKey(intsc);

            int x = 0;
            int xy = 0;

            int nBlock = intsc * 4;

            int a1 = intsc * 4;
            int a2 = a1 + 1;
            int a3 = a2 + 1;
            int a4 = a3 + 1;
            int a5 = a4 + 1;
            int a6 = a5 + 1;
            int a7 = a6 + 1;
            int a8 = a7 + 1;
            int a9 = a8 + 1;
            int a10 = a9 + 1;
            int a11 = a10 + 1;
            int a12 = a11 + 1;
            int a13 = a12 + 1;
            int a14 = a13 + 1;
            int a15 = a14 + 1;
            int a16 = a15 + 1;
            int a17 = a16 + 1;
            int a18 = a17 + 1;
            int a19 = a18 + 1;
            int a20 = a19 + 1;
            int a21 = a20 + 1;
            int a22 = a21 + 1;
            int a23 = a22 + 1;
            int a24 = a23 + 1;
            int a25 = a24 + 1;
            int a26 = a25 + 1;
            int a27 = a26 + 1;
            int a28 = a27 + 1;
            int a29 = a28 + 1;
            int a30 = a29 + 1;
            int a31 = a30 + 1;
            int a32 = a31 + 1;
            int a33 = a32 + 1;
            int a34 = a33 + 1;
            int a35 = a34 + 1;
            int a36 = a35 + 1;
            int a37 = a36 + 1;
            int a38 = a37 + 1;
            int a39 = a38 + 1;
            int a40 = a39 + 1;
            int a41 = a40 + 1;
            int a42 = a41 + 1;
            int a43 = a42 + 1;
            int a44 = a43 + 1;
            int a45 = a44 + 1;
            int a46 = a45 + 1;
            int a47 = a46 + 1;
            int a48 = a47 + 1;

            for (x = a1; x <= a24; x++)
            {
                String sdata = "";
                if (nBlock == a1)
                {
                    secno = GetSector((int)(a1));

                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    //System.Windows.Forms.Application.DoEvents();
                    
                    Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a1.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a2)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a2.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a3)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a3.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }

                //Key Block
                else if (nBlock == a4)
                {
                    //System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a4.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a4.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a4.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a5)
                {
                    secno = GetSector((int)(a5));

                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                   // System.Windows.Forms.Application.DoEvents();

                    Data = obj_Card.Authenticate(Convert.ToInt32(a5.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a5.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a5.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a6)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a6.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a7)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a7.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a8)
                {
                    //System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a8.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a8.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a8.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a9)
                {
                    secno = GetSector((int)(a9));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                  //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a9.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a9.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a9.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a10)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a10.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a11)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a11.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a12)
                {
                   // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a12.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a12.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a12.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a13)
                {
                    secno = GetSector((int)(a13));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                   // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a13.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a13.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a13.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a14)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a14.ToString()), sdata);
                    if (Data != "")
                        return "Authentication Error in " + nBlock;

                }

                else if (nBlock == a15)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a15.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }

                else if (nBlock == a16)
                {
                  //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a16.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a16.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a16.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }



                else if (nBlock == a17)
                {
                    secno = GetSector((int)(a17));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                   // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a17.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a17.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a17.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a18)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a18.ToString()), sdata);
                    if (Data != "")
                        return "Authentication Error in " + nBlock;

                }

                else if (nBlock == a19)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a19.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }

                else if (nBlock == a20)
                {
                  //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a20.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a20.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a20.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a21)
                {
                    secno = GetSector((int)(a21));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                  //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a21.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a21.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a21.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a22)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a22.ToString()), sdata);
                    if (Data != "")
                        return "Authentication Error in " + nBlock;

                }

                else if (nBlock == a23)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a23.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }

                else if (nBlock == a24)
                {
                  //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a24.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a24.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a24.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }

                nBlock++;
            }

            nBlock = a25;
            int y = 0;
            for (y = a25; y <= a48; y++)
            {
                String sdata;
                if (nBlock == a25)
                {
                    secno = GetSector((int)(a25));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                 //   System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a25.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a25.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a25.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a26)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a26.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a27)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a27.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                //Key Block
                else if (nBlock == a28)
                {
                  //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a28.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a28.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a28.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a29)
                {
                    secno = GetSector((int)(a29));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                  //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a29.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a29.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a29.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a30)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a31)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }

                //Key Block
                else if (nBlock == a32)
                {
                 //   System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a32.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a32.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a32.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a33)
                {
                    secno = GetSector((int)(a33));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                   // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a34)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }

                else if (nBlock == a35)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                //Key Block
                else if (nBlock == a36)
                {
                   // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a36.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a36.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a36.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a37)
                {
                    secno = GetSector((int)(a37));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                  //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a37.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a37.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a38)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a38.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a39)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                //Key Block
                else if (nBlock == a40)
                {
                   // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a40.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a40.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a40.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a41)
                {
                    secno = GetSector((int)(nBlock));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                   // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(nBlock.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(nBlock.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(nBlock.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a42)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(nBlock.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a43)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(nBlock.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                //Key Block
                else if (nBlock == a44)
                {
                  //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(nBlock.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(nBlock.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(nBlock.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a45)
                {
                    secno = GetSector((int)(nBlock));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                  //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(nBlock.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(nBlock.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(nBlock.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a46)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(nBlock.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a47)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(nBlock.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                //Key Block
                else if (nBlock == a48)
                {
                   // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(nBlock.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(nBlock.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(nBlock.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }

                //}
                nBlock++;
                //y++;
            }

            //objKCard.mifsw_exits(out llng1);
            //Data = obj_Card.DisConnectCard();
            //if (Data != "")
            //    return "Error in disconnecting";

            return ErrorMessage;
        
        }

        public string ISORawCard()
        {
            String ErrorMessage = "";
            String secno;         
            int intsc = 35;
            string Data = "";            
            String AccessKey;

            AccessKey = "FFFFFFFFFFFF";            
            intsc = 35;         

            int x = 0;
            int xy = 0;

            int nBlock = 176;

            int a1 = 176;
            int a2 = a1 + 1;
            int a3 = a2 + 1;
            int a4 = a3 + 1;
            int a5 = a4 + 1;
            int a6 = a5 + 1;
            int a7 = a6 + 1;
            int a8 = a7 + 1;
            int a9 = a8 + 1;
            int a10 = a9 + 1;
            int a11 = a10 + 1;
            int a12 = a11 + 1;
            int a13 = a12 + 1;
            int a14 = a13 + 1;
            int a15 = a14 + 1;
            int a16 = a15 + 1;
            int a17 = a16 + 1;
            int a18 = a17 + 1;
            int a19 = a18 + 1;
            int a20 = a19 + 1;
            int a21 = a20 + 1;
            int a22 = a21 + 1;
            int a23 = a22 + 1;
            int a24 = a23 + 1;
            int a25 = a24 + 1;
            int a26 = a25 + 1;
            int a27 = a26 + 1;
            int a28 = a27 + 1;
            int a29 = a28 + 1;
            int a30 = a29 + 1;
            int a31 = a30 + 1;
            int a32 = a31 + 1;
            int a33 = a32 + 1;
            int a34 = a33 + 1;
            int a35 = a34 + 1;
            int a36 = a35 + 1;
            int a37 = a36 + 1;
            int a38 = a37 + 1;
            int a39 = a38 + 1;
            int a40 = a39 + 1;
            int a41 = a40 + 1;
            int a42 = a41 + 1;
            int a43 = a42 + 1;
            int a44 = a43 + 1;
            int a45 = a44 + 1;
            int a46 = a45 + 1;
            int a47 = a46 + 1;
            int a48 = a47 + 1;           
            int a49 = a48 + 1;
            int a50 = a49 + 1;
            int a51 = a50 + 1;
            int a52 = a51 + 1;
            int a53 = a52 + 1;
            int a54 = a53 + 1;
            int a55 = a54 + 1;
            int a56 = a55 + 1;
            int a57 = a56 + 1;
            int a58 = a57 + 1;
            int a59 = a58 + 1;
            int a60 = a59 + 1;
            int a61 = a60 + 1;
            int a62 = a61 + 1;
            int a63 = a62 + 1;
            int a64 = a63 + 1;
            int a65 = a64 + 1;
            int a66 = a65 + 1;
            int a67 = a66 + 1;
            int a68 = a67 + 1;
            int a69 = a68 + 1;
            int a70 = a69 + 1;
            int a71 = a70 + 1;
            int a72 = a71 + 1;
            int a73 = a72 + 1;
            int a74 = a73 + 1;
            int a75 = a74 + 1;
            int a76 = a75 + 1;
            int a77 = a76 + 1;
            int a78 = a77 + 1;
            int a79 = a78 + 1;
            int a80 = a79 + 1;

            for (x = a1; x <= a80; x++)
            {
                String sdata = "";
                if (nBlock == a1)
                {
                    secno = GetSector((int)(a1));

                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    //System.Windows.Forms.Application.DoEvents();

                    Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a1.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a2)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a2.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a3)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a3.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }              
                else if (nBlock == a4)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a4.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a5)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a5.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a6)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a6.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a7)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a7.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a8)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a8.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a9)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a9.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a10)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a10.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a11)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a11.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a12)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a12.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a13)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a13.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a14)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a14.ToString()), sdata);
                    if (Data != "")
                        return "Authentication Error in " + nBlock;

                }
                else if (nBlock == a15)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a15.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a16)
                {
                    //  System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a16.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a16.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a16.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a17)
                {
                    secno = GetSector((int)(a17));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a17.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a17.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a17.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a18)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a18.ToString()), sdata);
                    if (Data != "")
                        return "Authentication Error in " + nBlock;

                }
                else if (nBlock == a19)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a19.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a20)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a20.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a21)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a21.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a22)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a22.ToString()), sdata);
                    if (Data != "")
                        return "Authentication Error in " + nBlock;

                }

                else if (nBlock == a23)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a23.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }

                else if (nBlock == a24)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a24.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a25)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a25.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a26)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a26.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a27)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a27.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a28)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a28.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a29)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a29.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a30)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a31)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                //Key Block
                else if (nBlock == a32)
                {
                    Data = obj_Card.Authenticate(Convert.ToInt32(a32.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a32.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a32.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a33)
                {
                    secno = GetSector((int)(a33));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a34)
                {                   
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a35)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a36)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a36.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a37)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a38)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a38.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a39)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a40)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a40.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a41)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a41.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a42)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a42.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a43)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a43.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a44)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a44.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a45)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a45.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                    
                }
                else if (nBlock == a46)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a46.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a47)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a47.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                //Key Block
                else if (nBlock == a48)
                {
                    Data = obj_Card.Authenticate(Convert.ToInt32(a48.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a48.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a48.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a49)
                {
                    secno = GetSector((int)(a49));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a49.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a49.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a49.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a50)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a50.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a51)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a51.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a52)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a52.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a53)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a53.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a54)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a54.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a55)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a55.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a56)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a56.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a57)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a57.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a58)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a58.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a59)
                {               
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a59.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a60)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a60.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a61)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a61.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a62)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a62.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a63)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a63.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                //Key Block
                else if (nBlock == a64)
                {
                    Data = obj_Card.Authenticate(Convert.ToInt32(a64.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a64.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a64.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }

                else if (nBlock == a65)
                {
                    secno = GetSector((int)(a65));
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    // System.Windows.Forms.Application.DoEvents();
                    Data = obj_Card.Authenticate(Convert.ToInt32(a65.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a65.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a65.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a66)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a66.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a67)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a67.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a68)
                {
                    sdata = "";

                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a68.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a69)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a69.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a70)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a70.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a71)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a71.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;
                }
                else if (nBlock == a72)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a72.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a73)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a73.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a74)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a74.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a75)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a75.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a76)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a76.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a77)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a77.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a78)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a78.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a79)
                {
                    sdata = "";
                    sdata = "30303030303030303030303030303030";

                    Data = obj_Card.WriteBlock((short)Convert.ToInt32(a79.ToString()), sdata);
                    if (Data != "")
                        return "Error in writing in " + nBlock;

                }
                else if (nBlock == a80)
                {
                    Data = obj_Card.Authenticate(Convert.ToInt32(a80.ToString()), strMasterKey, 96);
                    if (Data != "")
                    {
                        Data = obj_Card.Authenticate(Convert.ToInt32(a80.ToString()), AccessKey, 96);
                        if (Data != "")
                            return "Authentication Error in " + nBlock;
                    }

                    Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a80.ToString()), 1), strMasterKey, strMasterKey);
                    if (Data != "" && Data != null)
                        return "Error in writing in " + nBlock;
                }
                  

                nBlock++;
            }

            //nBlock = a20;
            //int y = 0;
            //for (y = a20; y <= a48; y++)
            //{
            //    String sdata;
            //     if (nBlock == a20)
            //     {
            //         secno = GetSector((int)(a1));
            //         sdata = "";
            //         sdata = "30303030303030303030303030303030";

            //         Data = obj_Card.Authenticate(Convert.ToInt32(a20.ToString()), strMasterKey, 96);
            //         if (Data != "")
            //         {
            //             Data = obj_Card.Authenticate(Convert.ToInt32(a20.ToString()), AccessKey, 96);
            //             if (Data != "")
            //                 return "Authentication Error in " + nBlock;
            //         }

            //         Data = obj_Card.WriteBlock((short)Convert.ToInt32(a20.ToString()), sdata);
            //         if (Data != "")
            //             return "Error in writing in " + nBlock;
                    
            //     }
            //     else if (nBlock == a21)
            //     {
            //         sdata = "";
            //         sdata = "30303030303030303030303030303030";

            //         Data = obj_Card.WriteBlock((short)Convert.ToInt32(a21.ToString()), sdata);
            //         if (Data != "")
            //             return "Error in writing in " + nBlock;
            //     }

            //     else if (nBlock == a22)
            //     {
            //         sdata = "";
            //         sdata = "30303030303030303030303030303030";

            //         Data = obj_Card.WriteBlock((short)Convert.ToInt32(a22.ToString()), sdata);
            //         if (Data != "")
            //             return "Authentication Error in " + nBlock;

            //     }
            //     else if (nBlock == a23)
            //     {
            //         sdata = "";
            //         sdata = "30303030303030303030303030303030";

            //         Data = obj_Card.WriteBlock((short)Convert.ToInt32(a23.ToString()), sdata);
            //         if (Data != "")
            //             return "Error in writing in " + nBlock;

            //     }
            //     else if (nBlock == a24)
            //     {
            //         sdata = "";
            //         sdata = "30303030303030303030303030303030";

            //         Data = obj_Card.WriteBlock((short)Convert.ToInt32(a24.ToString()), sdata);
            //         if (Data != "")
            //             return "Error in writing in " + nBlock;

            //     }
            //    else if (nBlock == a25)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a25.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;

            //    }
            //    else if (nBlock == a26)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a26.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }
            //    else if (nBlock == a27)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a27.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }               
            //    else if (nBlock == a28)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a28.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;

            //    }
            //    else if (nBlock == a29)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a29.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;


            //    }
            //    else if (nBlock == a30)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;

            //    }
            //    else if (nBlock == a31)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;

            //    }
            //    //Key Block
            //    else if (nBlock == a32)
            //    {
            //        //   System.Windows.Forms.Application.DoEvents();
            //        Data = obj_Card.Authenticate(Convert.ToInt32(a32.ToString()), strMasterKey, 96);
            //        if (Data != "")
            //        {
            //            Data = obj_Card.Authenticate(Convert.ToInt32(a32.ToString()), AccessKey, 96);
            //            if (Data != "")
            //                return "Authentication Error in " + nBlock;
            //        }

            //        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(a32.ToString()), 1), strMasterKey, strMasterKey);
            //        if (Data != "" && Data != null)
            //            return "Error in writing in " + nBlock;
            //    }
            //    else if (nBlock == a33)
            //    {
            //        secno = GetSector((int)(a33));
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        // System.Windows.Forms.Application.DoEvents();
            //        Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), strMasterKey, 96);
            //        if (Data != "")
            //        {
            //            Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), AccessKey, 96);
            //            if (Data != "")
            //                return "Authentication Error in " + nBlock;
            //        }

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    else if (nBlock == a34)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;

            //    }
            //    else if (nBlock == a35)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    //Key Block
            //    else if (nBlock == a36)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a36.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }
            //    else if (nBlock == a37)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    else if (nBlock == a38)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a38.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    else if (nBlock == a39)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    //Key Block
            //    else if (nBlock == a40)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a40.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }
            //    else if (nBlock == a41)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a41.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    else if (nBlock == a42)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a42.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    else if (nBlock == a43)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a43.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    //Key Block
            //    else if (nBlock == a44)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a44.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }
            //    else if (nBlock == a45)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a45.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    else if (nBlock == a46)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a46.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    else if (nBlock == a47)
            //    {
            //        sdata = "";
            //        sdata = "30303030303030303030303030303030";

            //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a47.ToString()), sdata);
            //        if (Data != "")
            //            return "Error in writing in " + nBlock;
            //    }

            //    //Key Block
            //    else if (nBlock == a48)
            //    {
            //        // System.Windows.Forms.Application.DoEvents();
            //        Data = obj_Card.Authenticate(Convert.ToInt32(nBlock.ToString()), strMasterKey, 96);
            //        if (Data != "")
            //        {
            //            Data = obj_Card.Authenticate(Convert.ToInt32(nBlock.ToString()), AccessKey, 96);
            //            if (Data != "")
            //                return "Authentication Error in " + nBlock;
            //        }

            //        Data = obj_Card.MakeSectorRaw(obj_Card.NumToHex(Convert.ToInt32(nBlock.ToString()), 1), strMasterKey, strMasterKey);
            //        if (Data != "" && Data != null)
            //            return "Error in writing in " + nBlock;
            //    }

            //    //}
            //    nBlock++;
            //    //y++;
            //}          

            return ErrorMessage;

        }

        public string ISOWriteCard(byte[] finger1,byte[] finger2,int IsoFingqty1,int IsoFingqty2)
        {
            try
            {

                String ErrorMessage = "";
                String secno;
                int intsc;
                String AccessKey;
                int BIRLength = 0;  
                ArrayList FingerTemplate = new ArrayList();   

                //string DataIntial = "";
                //DataIntial = obj_Card.Initialise().Trim();
                //if (DataIntial != "")
                //    return "Error in Initialization";

                string Data = "";
                //Data = obj_Card.ConnectToCard();
                //if (Data != "")
                //    return "Error in connecting";

                //if (strMasterKey == null)
                //{
                //    obj_Card.userid = "111111";
                //    obj_Card.pwd = "111111";
                //    strMasterKey = obj_Card.ReadMasterKey(obj_Card.userid, obj_Card.pwd);
                //    if (strMasterKey.Contains(" "))
                //    {
                //        string ErrorMsg = strMasterKey;
                //        strMasterKey = null;
                //        return ErrorMsg;
                //    }
                //    //Place User Card on Reader
                //}

                if (strMasterKey == null)
                {
                    strMasterKey = "FFFFFFFFFFFF";
                }

                Data = obj_Card.Initialise().Trim();
                if (Data != "")
                    return "Error in Initialization";

                Data = "";
                Data = obj_Card.ConnectToCard();
                if (Data != "")
                    return "Error in connecting";

                Data = obj_Card.LoadKey(strMasterKey).Trim();
                if (Data != "")
                    return "Error in Load Key";


                for (int cnt = 16; cnt > 20; cnt++)
                {
                    string pcard;
                    pcard = RawCard(cnt);

                    if (pcard != "")
                    {
                        return pcard;
                    }
                }


                string pcard1;
                pcard1 = ISORawCard();

                if (pcard1 != "")
                {
                    return pcard1;
                }

                AccessKey = "FFFFFFFFFFFF";

                CardSettings();
              //  intsc = ISOSector; commented sec 32

                intsc = 35;

                int x = 0;
                int xy = 0;

                int nBlock = 176;

                int a1 = 176;              

                int a2 = a1 + 1;
                int a3 = a2 + 1;
                int a4 = a3 + 1;
                int a5 = a4 + 1;
                int a6 = a5 + 1;
                int a7 = a6 + 1;
                int a8 = a7 + 1;
                int a9 = a8 + 1;
                int a10 = a9 + 1;
                int a11 = a10 + 1;
                int a12 = a11 + 1;
                int a13 = a12 + 1;
                int a14 = a13 + 1;
                int a15 = a14 + 1;
                int a16 = a15 + 1;
                int a17 = a16 + 1;
                int a18 = a17 + 1;
                int a19 = a18 + 1;
                int a20 = a19 + 1;
                int a21 = a20 + 1;
                int a22 = a21 + 1;
                int a23 = a22 + 1;
                int a24 = a23 + 1;
                int a25 = a24 + 1;
                int a26 = a25 + 1;
                int a27 = a26 + 1;
                int a28 = a27 + 1;
                int a29 = a28 + 1;
                int a30 = a29 + 1;
                int a31 = a30 + 1;
                int a32 = a31 + 1;
                int a33 = a32 + 1;
                int a34 = a33 + 1;
                int a35 = a34 + 1;
                int a36 = a35 + 1;
                int a37 = a36 + 1;
                int a38 = a37 + 1;
                int a39 = a38 + 1;
                int a40 = a39 + 1;
                int a41 = a40 + 1;
                int a42 = a41 + 1;
                int a43 = a42 + 1;
                int a44 = a43 + 1;
                int a45 = a44 + 1;
                int a46 = a45 + 1;
                int a47 = a46 + 1;
                int a48 = a47 + 1;
                int a49 = a48 + 1;
                int a50 = a49 + 1;
                int a51 = a50 + 1;
                int a52 = a51 + 1;
                int a53 = a52 + 1;
                int a54 = a53 + 1;
                int a55 = a54 + 1;
                int a56 = a55 + 1;
                int a57 = a56 + 1;
                int a58 = a57 + 1;
                int a59 = a58 + 1;
                int a60 = a59 + 1;
                int a61 = a60 + 1;
                int a62 = a61 + 1;
                int a63 = a62 + 1;
                int a64 = a63 + 1;
                int a65 = a64 + 1;
                int a66 = a65 + 1;
                int a67 = a66 + 1;
                int a68 = a67 + 1;
                int a69 = a68 + 1;
                int a70 = a69 + 1;
                int a71 = a70 + 1;
                int a72 = a71 + 1;
                int a73 = a72 + 1;
                int a74 = a73 + 1;
                int a75 = a74 + 1;
                int a76 = a75 + 1;
                int a77 = a76 + 1;
                int a78 = a77 + 1;
                int a79 = a78 + 1;
                int a80 = a79 + 1;

                //********* According to BIR Storage format excluding 2 finger minutiae data size,constatnt Length is 22 **********
                BIRLength = 534;
                //BIRLength += finger1.Length;
                //BIRLength += finger2.Length;
                //*********end                       

                int AllData = 0;
                int d1 = 0;
                string AllData1 = "";
                int intsleep = 500;
                int Totalfingerqty = (IsoFingqty1 + IsoFingqty2) / 2;
                String Finger1No = Bin.Converse.IntToHex(fingerIndex1, 1).ToString();
                String Finger1Qty = Bin.Converse.IntToHex(IsoFingqty1, 1).ToString();
                String Finger2No = Bin.Converse.IntToHex(fingerIndex2, 1).ToString();
                String Finger2Qty = Bin.Converse.IntToHex(IsoFingqty2, 1).ToString();


                FingerTemplate.Add("42");
                FingerTemplate.Add("44");
                FingerTemplate.Add("52");
                FingerTemplate.Add("00");
                FingerTemplate.Add("31");
                FingerTemplate.Add("2E");
                FingerTemplate.Add("30");
                FingerTemplate.Add("00");
                FingerTemplate.Add(Bin.Converse.IntToHex(Totalfingerqty, 1).ToString());
                FingerTemplate.Add("02");
                FingerTemplate.Add("01");
                FingerTemplate.Add("FF");
                FingerTemplate.Add("FF");
                FingerTemplate.Add("FF");
                FingerTemplate.Add("FF");
                FingerTemplate.Add("FF");
                FingerTemplate.Add("FF");
                FingerTemplate.Add("FF");
                FingerTemplate.Add("FF");
                FingerTemplate.Add("FF");
                FingerTemplate.Add(Finger1No);
                FingerTemplate.Add(Finger1Qty);
                FingerTemplate.Add(Bin.Converse.IntToHex(finger1.Length, 2).ToString().Substring(0,2));
                FingerTemplate.Add(Bin.Converse.IntToHex(finger1.Length, 2).ToString().Substring(2,2));

                 for (int cnt = 0; cnt < finger1.Length; cnt++)
                {
                    FingerTemplate.Add(Bin.Converse.IntToHex(finger1[cnt],1).ToString());
                }

                if(finger1.Length < 512 )
                {
                    for (int i = 0; i < 512 - finger1.Length; i++)
                    {
                         FingerTemplate.Add("30");
                    }
                }

                FingerTemplate.Add("FF");
                FingerTemplate.Add("FF");

                FingerTemplate.Add(Finger2No);
                FingerTemplate.Add(Finger2Qty);
                FingerTemplate.Add(Bin.Converse.IntToHex(finger2.Length, 2).ToString().Substring(0,2));
                FingerTemplate.Add(Bin.Converse.IntToHex(finger2.Length, 2).ToString().Substring(2, 2));
                
                 for (int cnt1 = 0; cnt1 < finger2.Length; cnt1++)
                {
                    FingerTemplate.Add(Bin.Converse.IntToHex(finger2[cnt1],1).ToString());
                }

                if(finger2.Length < 512 )
                {
                    for (int j = 0; j < 512 - finger2.Length; j++)
                    {
                         FingerTemplate.Add("30");
                    }
                }

                FingerTemplate.Add("FF");
                FingerTemplate.Add("FF");


                for (x = 0; x < FingerTemplate.Count; x++)
                {
                    String sdata = "";
                    if (nBlock == a1)
                    {
                        secno = GetSector((int)(a1));

                      //  String FormatIdentifier = "42445200"; // 4 Bytes Identifier
                      //  String VersionNo = "312E3000";       // 4 Bytes Version No
                      ////  String BioDataFormat = "0203"; 
                      //  int Totalfingerqty = (IsoFingqty1 + IsoFingqty2) / 2;
                      //  String OverallQuality = Bin.Converse.IntToHex(Totalfingerqty, 1).ToString();    // 1 byte Biometric Quality
                      //  String NoofFingers = "02";      // 1 byte no of templates
                      ////  String LenOfBIR = Bin.Converse.IntToHex(BIRLength, 1).ToString();
                      //  string SensorUsed = "01";       // 1 byte Sensor Used
                      //  string RFU = "FFFFFFFFFFFFFFFFFFF"; //9 Bytes RFU

                        sdata = "";

                        for (x = 0; x <= 15; x++)
                        {                            
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        System.Threading.Thread.Sleep(intsleep);

                        Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), strMasterKey, 97);
                            if (Data != "")
                            {
                                Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), AccessKey, 96);
                                if (Data != "")
                                {
                                    System.Threading.Thread.Sleep(intsleep);
                                    Data = obj_Card.Authenticate(Convert.ToInt32(a1.ToString()), AccessKey, 97);
                                    if (Data != "")
                                        return "Authentication Error in Block " + a1;
                                }
                            }
                        }
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a1.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a1;
                    }
                    else if (nBlock == a2)
                    {
                        //String Finger1No = Bin.Converse.IntToHex(fingerIndex1, 1).ToString();
                        //String Finger1Qty = Bin.Converse.IntToHex(IsoFingqty1, 1).ToString();

                        sdata = "";
                        for (x = 16; x <= 31; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a2.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a2;
                    }
                    else if (nBlock == a3)
                    {
                        sdata = "";
                        for (x = 32; x <= 47; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a3.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a3;

                    }
                    else if (nBlock == a4)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 63)
                        {
                            xy = 63;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 48; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a4.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a4;

                    }
                    else if (nBlock == a5)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 79)
                        {
                            xy = 79;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 64; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a5.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a5;

                    }
                    else if (nBlock == a6)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 95)
                        {
                            xy = 95;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 80; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a6.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a6;
                    }
                    else if (nBlock == a7)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 111)
                        {
                            xy = 111;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }

                        for (x = 96; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a7.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a7;

                    }
                    else if (nBlock == a8)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 127)
                        {
                            xy = 127;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 112; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a8.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a8;
                    }
                    else if (nBlock == a9)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 143)
                        {
                            xy = 143;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 128; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a9.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a9;
                    }

                    else if (nBlock == a10)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 159)
                        {
                            xy = 159;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 144; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a10.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a10;
                    }
                    else if (nBlock == a11)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 175)
                        {
                            xy = 175;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 160; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a11.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a11;
                    }
                    else if (nBlock == a12)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 191)
                        {
                            xy = 191;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 176; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a12.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a12;
                    }
                    else if (nBlock == a13)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 207)
                        {
                            xy = 207;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 192; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a13.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a13;

                    }
                    else if (nBlock == a14)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 223)
                        {
                            xy = 223;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 208; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a14.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a14;

                    }
                    else if (nBlock == a15)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 239)
                        {
                            xy = 239;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 224; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a15.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a15;
                    }
                    //Key Block
                    else if (nBlock == a16)
                    {

                    }
                    else if (nBlock == a17)
                    {
                        secno = GetSector((int)(a17));
                        sdata = "";
                        if (FingerTemplate.Count > 255)
                        {
                            xy = 255;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 240; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.Authenticate(Convert.ToInt32(a17.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a17.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a17;
                        }
                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a17.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a17;
                    }
                    else if (nBlock == a18)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 271)
                        {
                            xy = 271;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 256; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a18.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a18;

                    }
                    else if (nBlock == a19)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 287)
                        {
                            xy = 287;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 272; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a19.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a19;

                    }
                    else if (nBlock == a20)
                    {                      
                        sdata = "";
                        if (FingerTemplate.Count > 303)
                        {
                            xy = 303;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 288; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }                        

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a20.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a20;
                    }
                    else if (nBlock == a21)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 319)
                        {
                            xy = 319;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 304; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }        

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a21.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a21;
                    }

                    else if (nBlock == a22)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 335)
                        {
                            xy = 335;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 320; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }      

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a22.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a22;

                    }
                    else if (nBlock == a23)
                    {
                        secno = GetSector((int)(a23));
                        sdata = "";
                        if (FingerTemplate.Count > 351)
                        {
                            xy = 351;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 336; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a23.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a23;
                    }
                    else if (nBlock == a24)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 367)
                        {
                            xy = 367;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 352; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a24.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a24;

                    }
                    else if (nBlock == a25)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 383)
                        {
                            xy = 383;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 368; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a25.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a25;
                    }
                    else if (nBlock == a26)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 399)
                        {
                            xy = 399;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 384; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a26.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a26;
                    }
                    else if (nBlock == a27)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 415)
                        {
                            xy = 415;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 400; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a27.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a27;
                    }
                    else if (nBlock == a28)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 431)
                        {
                            xy = 431;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 416; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a28.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a28;

                    }
                    else if (nBlock == a29)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 447)
                        {
                            xy = 447;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 432; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a29.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a29;

                    }
                    else if (nBlock == a30)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 463)
                        {
                            xy = 463;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 448; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a30;
                    }
                    else if (nBlock == a31)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 479)
                        {
                            xy = 479;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 464; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a31;

                    }
                    //Key Block
                    else if (nBlock == a32)
                    {

                    }
                    else if (nBlock == a33)
                    {
                        secno = GetSector((int)(a33));
                        sdata = "";

                        if (FingerTemplate.Count > 495)
                        {
                            xy = 495;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 480; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a33;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a33;
                    }

                    else if (nBlock == a34)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 511)
                        {
                            xy = 511;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 496; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a34;

                    }
                    else if (nBlock == a35)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 527)
                        {
                            xy = 527;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 512; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   


                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a35;
                    }
                    else if (nBlock == a36)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 543)
                        {
                            xy = 543;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 528; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a36.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a36;

                    }
                    else if (nBlock == a37)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 559)
                        {
                            xy = 559;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 544; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }   

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a37;
                    }

                    else if (nBlock == a38)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 575)
                        {
                            xy = 575;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 560; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a38.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a38;
                    }

                    else if (nBlock == a39)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 591)
                        {
                            xy = 591;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 576; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a39.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a39;
                    }
                    else if (nBlock == a40)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 607)
                        {
                            xy = 607;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 592; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a40.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a40;
                    }
                    else if (nBlock == a41)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 623)
                        {
                            xy = 623;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 608; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a41.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a41;
                    }
                    else if (nBlock == a42)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 639)
                        {
                            xy = 639;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 624; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a42.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a42;
                    }
                    else if (nBlock == a43)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 655)
                        {
                            xy = 655;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 640; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a43.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a43;
                    }
                    else if (nBlock == a44)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 671)
                        {
                            xy = 671;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 656; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a44.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a44;
                    }
                    else if (nBlock == a45)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 687)
                        {
                            xy = 687;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 672; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a45.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a45;
                    }
                    else if (nBlock == a46)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 703)
                        {
                            xy = 703;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 688; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a46.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a46;
                    }
                    else if (nBlock == a47)
                    {
                        sdata = "";
                        if (FingerTemplate.Count > 719)
                        {
                            xy = 719;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 704; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a47.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a47;
                    }
                    //Key Block
                    else if (nBlock == a48)
                    {

                    }
                    else if (nBlock == a49)
                    {
                        secno = GetSector((int)(a49));
                        sdata = "";

                        if (FingerTemplate.Count > 735)
                        {
                            xy = 735;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 720; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a49.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a49.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a49;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a49.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a49;
                    }

                    else if (nBlock == a50)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 751)
                        {
                            xy = 751;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 736; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a50.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a50;

                    }
                    else if (nBlock == a51)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 767)
                        {
                            xy = 767;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 752; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a51.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a51;

                    }
                    else if (nBlock == a52)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 783)
                        {
                            xy = 783;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 768; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a52.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a52;

                    }
                    else if (nBlock == a53)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 799)
                        {
                            xy = 799;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 784; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a53.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a53;

                    }
                    else if (nBlock == a54)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 815)
                        {
                            xy = 815;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 800; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a54.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a54;

                    }
                    else if (nBlock == a55)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 831)
                        {
                            xy = 831;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 816; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a55.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a55;

                    }
                    else if (nBlock == a56)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 847)
                        {
                            xy = 847;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 832; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a56.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a56;

                    }
                    else if (nBlock == a57)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 863)
                        {
                            xy = 863;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 848; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a57.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a57;

                    }
                    else if (nBlock == a58)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 879)
                        {
                            xy = 879;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 864; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a58.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a58;

                    }
                    else if (nBlock == a59)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 895)
                        {
                            xy = 895;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 880; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a59.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a59;

                    }
                    else if (nBlock == a60)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 911)
                        {
                            xy = 911;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 896; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a60.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a60;

                    }
                    else if (nBlock == a61)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 927)
                        {
                            xy = 927;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 912; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a61.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a61;

                    }
                    else if (nBlock == a62)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 943)
                        {
                            xy = 943;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 928; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a62.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a62;

                    }
                    else if (nBlock == a63)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 959)
                        {
                            xy = 959;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 944; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a63.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a63;

                    }
                    //Key Block
                    else if (nBlock == a64)
                    {
                        
                    }
                    else if (nBlock == a65)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 975)
                        {
                            xy = 975;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 960; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        System.Threading.Thread.Sleep(intsleep);
                        Data = obj_Card.Authenticate(Convert.ToInt32(a65.ToString()), strMasterKey, 96);
                        if (Data != "")
                        {
                            System.Threading.Thread.Sleep(intsleep);
                            Data = obj_Card.Authenticate(Convert.ToInt32(a65.ToString()), AccessKey, 96);
                            if (Data != "")
                                return "Authentication Error in Block " + a65;
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a65.ToString()), sdata.PadRight(32, '0'));
                        //}
                        if (Data != "")
                            return "Error in Writing in Block " + a65;

                    }
                    else if (nBlock == a66)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 991)
                        {
                            xy = 991;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 976; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a66.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a66;

                    }
                    else if (nBlock == a67)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1007)
                        {
                            xy = 1007;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 992; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a67.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a67;

                    }
                    else if (nBlock == a68)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1023)
                        {
                            xy = 1023;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1008; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a68.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a68;

                    }
                    else if (nBlock == a69)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1039)
                        {
                            xy = 1039;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1024; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a69.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a69;

                    }
                    else if (nBlock == a70)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1055)
                        {
                            xy = 1055;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1040; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a70.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a70;

                    }
                    else if (nBlock == a71)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1071)
                        {
                            xy = 1071;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1056; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a71.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a71;

                    }
                    else if (nBlock == a72)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1087)
                        {
                            xy = 1087;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1072; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a72.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a72;

                    }
                    else if (nBlock == a73)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1103)
                        {
                            xy = 1103;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1088; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a73.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a73;

                    }
                    else if (nBlock == a74)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1119)
                        {
                            xy = 1119;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1104; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a74.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a74;

                    }
                    else if (nBlock == a75)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1135)
                        {
                            xy = 1135;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1120; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a75.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a75;

                    }
                    else if (nBlock == a76)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1151)
                        {
                            xy = 1151;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1136; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a76.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a76;

                    }
                    else if (nBlock == a77)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1167)
                        {
                            xy = 1167;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1152; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a77.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a77;

                    }
                    else if (nBlock == a78)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1183)
                        {
                            xy = 1183;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1168; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a78.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a78;

                    }
                    else if (nBlock == a79)
                    {
                        sdata = "";

                        if (FingerTemplate.Count > 1199)
                        {
                            xy = 1199;
                        }
                        else
                        {
                            xy = FingerTemplate.Count - 1;
                        }
                        for (x = 1184; x <= xy; x++)
                        {
                            sdata = sdata + FingerTemplate[x].ToString();
                        }

                        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a79.ToString()), sdata.PadRight(32, '0'));
                        if (Data != "")
                            return "Error in Writing in Block " + a79;

                    }
                    else if (nBlock == a80)
                    {
                        
                    }
                    
                    nBlock++;
                }

                //nBlock = a20;
                //int y = 0;
                //String Finger2No = Bin.Converse.IntToHex(fingerIndex2, 1).ToString();
                //String Finger2Qty = Bin.Converse.IntToHex(IsoFingqty2, 1).ToString();
                //for (y = 0; y <= finger2.Length + 1; y++)
                //{
                //    String sdata;
                //    if (nBlock == a20)
                //    {
                //        secno = GetSector((int)(a20));
                //        sdata = "";
                //        sdata = Finger2No + Finger2Qty + Bin.Converse.IntToHex(finger2.Length, 2).ToString();

                //        Data = obj_Card.Authenticate(Convert.ToInt32(a20.ToString()), strMasterKey, 96);
                //        if (Data != "")
                //        {
                //            System.Threading.Thread.Sleep(intsleep);
                //            Data = obj_Card.Authenticate(Convert.ToInt32(a20.ToString()), AccessKey, 96);
                //            if (Data != "")
                //                return "Authentication Error in Block " + a20;
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a20.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a20;
                //    }
                //    else if (nBlock == a21)
                //    {
                //        sdata = "";
                //        for (y = 0; y <= 15; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a21.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a21;
                //    }

                //    else if (nBlock == a22)
                //    {
                //        sdata = "";
                //        for (y = 16; y <= 31; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a22.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a22;

                //    }
                //    else if (nBlock == a23)
                //    {
                //        secno = GetSector((int)(a23));
                //        sdata = "";
                //        for (y = 32; y <= 47; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a23.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a23;
                //    }
                //    else if (nBlock == a24)
                //    {
                //        sdata = "";
                //        for (y = 48; y <= 63; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a24.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a24;

                //    }
                //    else if (nBlock == a25)
                //    {
                //        sdata = "";

                //        if (finger2.Length > 79)
                //        {
                //            xy = 79;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 64; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a25.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a25;
                //    }
                //    else if (nBlock == a26)
                //    {
                //        sdata = "";

                //        if (finger2.Length > 95)
                //        {
                //            xy = 95;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 80; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a26.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a26;
                //    }
                //    else if (nBlock == a27)
                //    {
                //        sdata = "";

                //        if (finger2.Length > 111)
                //        {
                //            xy = 111;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 96; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a27.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a27;
                //    }
                //    else if (nBlock == a28)
                //    {
                //        sdata = "";

                //        if (finger2.Length > 127)
                //        {
                //            xy = 127;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 112; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a28.ToString()), sdata.PadRight(32, '0'));
                //        //}
                //        if (Data != "")
                //            return "Error in Writing in Block " + a28;

                //    }
                //    else if (nBlock == a29)
                //    {
                //        sdata = "";

                //        if (finger2.Length > 143)
                //        {
                //            xy = 143;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 128; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a29.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a29;

                //    }
                //    else if (nBlock == a30)
                //    {
                //        sdata = "";

                //        if (finger2.Length > 159)
                //        {
                //            xy = 159;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 144; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a30.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a30;
                //    }
                //    else if (nBlock == a31)
                //    {
                //        sdata = "";

                //        if (finger2.Length > 175)
                //        {
                //            xy = 175;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 160; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a31.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a31;

                //    }
                //    //Key Block
                //    else if (nBlock == a32)
                //    {

                //    }
                //    else if (nBlock == a33)
                //    {
                //        secno = GetSector((int)(a33));
                //        sdata = "";

                //        if (finger2.Length > 191)
                //        {
                //            xy = 191;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 176; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        System.Threading.Thread.Sleep(intsleep);
                //        Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), strMasterKey, 96);
                //        if (Data != "")
                //        {
                //            System.Threading.Thread.Sleep(intsleep);
                //            Data = obj_Card.Authenticate(Convert.ToInt32(a33.ToString()), AccessKey, 96);
                //            if (Data != "")
                //                return "Authentication Error in Block " + a33;
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a33.ToString()), sdata.PadRight(32, '0'));
                //        //}
                //        if (Data != "")
                //            return "Error in Writing in Block " + a33;
                //    }

                //    else if (nBlock == a34)
                //    {
                //        sdata = "";

                //        if (finger2.Length > 207)
                //        {
                //            xy = 207;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 192; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a34.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a34;

                //    }
                //    else if (nBlock == a35)
                //    {
                //        sdata = "";

                //        if (finger2.Length > 223)
                //        {
                //            xy = 223;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 208; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a35.ToString()), sdata.PadRight(32, '0'));
                //        //}
                //        if (Data != "")
                //            return "Error in Writing in Block " + a35;
                //    }
                //    else if (nBlock == a36)
                //    {
                //        sdata = "";

                //        if (finger2.Length > 239)
                //        {
                //            xy = 239;
                //        }
                //        else
                //        {
                //            xy = finger2.Length - 1;
                //        }

                //        for (y = 224; y <= xy; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString();
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a36.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a36;

                //    }
                //    else if (nBlock == a37)
                //    {
                //        sdata = "";
                //        for (y = 240; y <= finger1.Length - 1; y++)
                //        {
                //            sdata = sdata + Bin.Converse.IntToHex(finger2[y], 1).ToString(); ;
                //            d1 = Convert.ToInt16(finger2[y]);
                //            AllData = AllData + d1;
                //        }

                //        Data = obj_Card.WriteBlock((short)Convert.ToInt32(a37.ToString()), sdata.PadRight(32, '0'));
                //        if (Data != "")
                //            return "Error in Writing in Block " + a37;
                //    }

                //    else if (nBlock == a38)
                //    {

                //    }

                //    else if (nBlock == a39)
                //    {

                //    }
                //    else if (nBlock == a40)
                //    {

                //    }
                //    else if (nBlock == a41)
                //    {

                //    }
                //    else if (nBlock == a42)
                //    {

                //    }
                //    else if (nBlock == a43)
                //    {

                //    }
                //    else if (nBlock == a44)
                //    {

                //    }
                //    else if (nBlock == a45)
                //    {

                //    }
                //    else if (nBlock == a46)
                //    {

                //    }
                //    else if (nBlock == a47)
                //    {

                //    }
                //    //Key Block
                //    else if (nBlock == a48)
                //    {

                //    }
                //    //}
                //    nBlock++;
                    //y++;
                //}

                return ErrorMessage;
            }
            catch (Exception ex)
            {
               // throw ex;
                clsCardRW.Execute = false;                
                return ex.Message;
            }

        }

        public void CardSettings()
        {
           string strsql = "Select * from BioMatricCardSetting";
           SqlDataAdapter da = new SqlDataAdapter(strsql, AccessController.m_connecton);
           DataTable dtsettings = new DataTable();
           da.Fill(dtsettings);
           if (dtsettings.Rows.Count != 0)
           {
               FingerQty = Convert.ToInt32(dtsettings.Rows[0]["FingerQualityScore"]);
               NoOfRetries = Convert.ToInt32(dtsettings.Rows[0]["NoOfRetries"]);
               TimeOut = Convert.ToInt32(dtsettings.Rows[0]["Timeout"]);
               fingerIndex1 = Convert.ToInt32(dtsettings.Rows[0]["FVAL"]);
               fingerIndex2 = Convert.ToInt32(dtsettings.Rows[0]["SVAL"]);
               finger_val1 = "TempMin" + dtsettings.Rows[0]["FVAL"].ToString();
               finger_val2 = "TempMin" + dtsettings.Rows[0]["SVAL"].ToString();
               ISOfinger_val1 = "ISOTempMin" + dtsettings.Rows[0]["FVAL"].ToString();
               ISOfinger_val2 = "ISOTempMin" + dtsettings.Rows[0]["SVAL"].ToString();
               NativeSector = Convert.ToInt32(dtsettings.Rows[0]["NativeSector"]);
               ISOSector = Convert.ToInt32(dtsettings.Rows[0]["ISOSector"]);
           }
          
          
        }

       
    }
}