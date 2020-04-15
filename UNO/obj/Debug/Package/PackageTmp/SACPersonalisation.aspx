<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="SACPersonalisation.aspx.cs" Inherits="UNO.SACPersonalisation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="JavaScript">
        // accumulate values to put into text area
        var accumulated_output_info;

        // add a labeled value to the text area
        function accumulate_output(str) {
            accumulated_output_info = accumulated_output_info + str + "\n";
        }

        // add a bit string to the output, inserting spaces as designated
        function accumulate_bitstring(label, ary, spacing) {
            var i;

            accumulated_output_info += label;

            // add bits
            for (i = 1; i < ary.length; i++) {
                if ((i % spacing) == 1)
                    accumulated_output_info += " "; // time to add a space
                accumulated_output_info += ary[i]; // and the bit
            }

            // insert trailing end-of-line
            accumulated_output_info += "\n";
        }

        // special value stored in x[0] to indicate a problem
        var ERROR_VAL = -9876;

        // initial permutation (split into left/right halves )
        // since DES numbers bits starting at 1, we will ignore x[0]
        var IP_perm = new Array(-1,
	58, 50, 42, 34, 26, 18, 10, 2,
	60, 52, 44, 36, 28, 20, 12, 4,
	62, 54, 46, 38, 30, 22, 14, 6,
	64, 56, 48, 40, 32, 24, 16, 8,
	57, 49, 41, 33, 25, 17, 9, 1,
	59, 51, 43, 35, 27, 19, 11, 3,
	61, 53, 45, 37, 29, 21, 13, 5,
	63, 55, 47, 39, 31, 23, 15, 7);

        // final permutation (inverse initial permutation)
        var FP_perm = new Array(-1,
	40, 8, 48, 16, 56, 24, 64, 32,
	39, 7, 47, 15, 55, 23, 63, 31,
	38, 6, 46, 14, 54, 22, 62, 30,
	37, 5, 45, 13, 53, 21, 61, 29,
	36, 4, 44, 12, 52, 20, 60, 28,
	35, 3, 43, 11, 51, 19, 59, 27,
	34, 2, 42, 10, 50, 18, 58, 26,
	33, 1, 41, 9, 49, 17, 57, 25);

        // per-round expansion
        var E_perm = new Array(-1,
	32, 1, 2, 3, 4, 5,
	4, 5, 6, 7, 8, 9,
	8, 9, 10, 11, 12, 13,
	12, 13, 14, 15, 16, 17,
	16, 17, 18, 19, 20, 21,
	20, 21, 22, 23, 24, 25,
	24, 25, 26, 27, 28, 29,
	28, 29, 30, 31, 32, 1);

        // per-round permutation
        var P_perm = new Array(-1,
	16, 7, 20, 21, 29, 12, 28, 17,
	1, 15, 23, 26, 5, 18, 31, 10,
	2, 8, 24, 14, 32, 27, 3, 9,
	19, 13, 30, 6, 22, 11, 4, 25);

        // note we do use element 0 in the S-Boxes
        var S1 = new Array(
	14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7,
	0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8,
	4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0,
	15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13);
        var S2 = new Array(
	15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10,
	3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5,
	0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15,
	13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9);
        var S3 = new Array(
	10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8,
	13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1,
	13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7,
	1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12);
        var S4 = new Array(
	7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15,
	13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9,
	10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4,
	3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14);
        var S5 = new Array(
	2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9,
	14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6,
	4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14,
	11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3);
        var S6 = new Array(
	12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11,
	10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8,
	9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6,
	4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13);
        var S7 = new Array(
	4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1,
	13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6,
	1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2,
	6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12);
        var S8 = new Array(
	13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7,
	1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2,
	7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8,
	2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11);

        //, first, key, permutation
        var PC_1_perm = new Array(-1,
        // C subkey bits
	57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18,
	10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36,
        // D subkey bits
	63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22,
	14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4);

        //, per-round, key, selection, permutation
        var PC_2_perm = new Array(-1,
	14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10,
	23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2,
	41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48,
	44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32);

        // save output in case we want to reformat it later
        var DES_output = new Array(65);

        // remove spaces from input
        function remove_spaces(instr) {
            var i;
            var outstr = "";

            for (i = 0; i < instr.length; i++)
                if (instr.charAt(i) != " ")
                // not a space, include it
                    outstr += instr.charAt(i);

            return outstr;
        }

        // split an integer into bits
        // ary   = array to store bits in
        // start = starting subscript
        // bitc  = number of bits to convert
        // val   = number to convert
        function split_int(ary, start, bitc, val) {
            var i = start;
            var j;
            for (j = bitc - 1; j >= 0; j--) {
                // isolate low-order bit
                ary[i + j] = val & 1;
                // remove that bit
                val >>= 1;
            }
        }

        // get the message to encrypt/decrypt
        function get_value(bitarray, str, isASCII) {
            var i;
            var val; // one hex digit

            // insert note we probably are ok
            bitarray[0] = -1;

            if (isASCII) {
                // check length of data
                if (str.length != 8) {
                    window.alert("Message and key must be 64 bits (8 ASCII characters)");
                    bitarray[0] = ERROR_VAL;
                    return
                }

                // have ASCII data
                for (i = 0; i < 8; i++) {
                    split_int(bitarray, i * 8 + 1, 8, str.charCodeAt(i));
                }
            }
            else {
                // have hex data - remove any spaces they used, then convert
                str = remove_spaces(str);

                // check length of data
                if (str.length != 16) {
                    window.alert("Message and key must be 64 bits (16 hex digits)");
                    bitarray[0] = ERROR_VAL;
                    return;
                }

                for (i = 0; i < 16; i++) {
                    // get the next hex digit
                    val = str.charCodeAt(i);

                    // do some error checking
                    if (val >= 48 && val <= 57)
                    // have a valid digit 0-9
                        val -= 48;
                    else if (val >= 65 && val <= 70)
                    // have a valid digit A-F
                        val -= 55;
                    else if (val >= 97 && val <= 102)
                    // have a valid digit A-F
                        val -= 87;
                    else {
                        // not 0-9 or A-F, complain
                        window.alert(str.charAt(i) + " is not a valid hex digit");
                        bitarray[0] = ERROR_VAL;
                        return;
                    }

                    // add this digit to the array
                    split_int(bitarray, i * 4 + 1, 4, val - 48);
                }
            }
        }

        // format the output in the desired form
        // if -1, use existing value
        // -- uses the global array DES_output
        function format_DES_output() {
            var i;
            var bits;
            var str = "";

            // what type of data do we have to work with?
            if (false) {
                // convert each set of bits back to ASCII
                for (i = 1; i <= 64; i += 8) {
                    str += String.fromCharCode(
                        DES_output[i] * 128 + DES_output[i + 1] * 64 +
                        DES_output[i + 2] * 32 + DES_output[i + 3] * 16 +
                        DES_output[i + 4] * 8 + DES_output[i + 5] * 4 +
                        DES_output[i + 6] * 2 + DES_output[i + 7]);
                }
            }
            else {
                // output hexdecimal data
                for (i = 1; i <= 64; i += 4) {
                    bits = DES_output[i] * 8 + DES_output[i + 1] * 4 +
                   DES_output[i + 2] * 2 + DES_output[i + 3];

                    // 0-9 or A-F?
                    if (bits <= 9)
                        str += String.fromCharCode(bits + 48);
                    else
                        str += String.fromCharCode(bits + 87);
                }
            }
            // copy to textbox
            return str;
        }

        // copy bits in a permutation
        //   dest = where to copy the bits to
        //   src  = Where to copy the bits from
        //   perm = The order to copy/permute the bits
        // note: since DES ingores x[0], we do also
        function permute(dest, src, perm) {
            var i;
            var fromloc;

            for (i = 1; i < perm.length; i++) {
                fromloc = perm[i];
                dest[i] = src[fromloc];
            }
        }

        // do an array XOR
        // assume all array entries are 0 or 1
        function xor(a1, a2) {
            var i;

            for (i = 1; i < a1.length; i++)
                a1[i] = a1[i] ^ a2[i];
        }

        // process one S-Box, return integer from S-Box
        function do_S(SBox, index, inbits) {
            // collect the 6 bits into a single integer
            var S_index = inbits[index] * 32 + inbits[index + 5] * 16 +
                 inbits[index + 1] * 8 + inbits[index + 2] * 4 +
                 inbits[index + 3] * 2 + inbits[index + 4];

            // do lookup
            return SBox[S_index];
        }

        // do one round of DES encryption
        function des_round(L, R, KeyR) {
            var E_result = new Array(49);
            var S_out = new Array(33);

            // copy the existing L bits, then set new L = old R
            var temp_L = new Array(33);
            for (i = 0; i < 33; i++) {
                // copy exising L bits
                temp_L[i] = L[i];

                // set L = R
                L[i] = R[i];
            }

            // expand R using E permutation
            permute(E_result, R, E_perm);
            accumulate_bitstring("E   : ", E_result, 6);
            accumulate_bitstring("KS  : ", KeyR, 6);

            // exclusive-or with current key
            xor(E_result, KeyR);
            accumulate_bitstring("E xor KS: ", E_result, 6);

            // put through the S-Boxes
            split_int(S_out, 1, 4, do_S(S1, 1, E_result));
            split_int(S_out, 5, 4, do_S(S2, 7, E_result));
            split_int(S_out, 9, 4, do_S(S3, 13, E_result));
            split_int(S_out, 13, 4, do_S(S4, 19, E_result));
            split_int(S_out, 17, 4, do_S(S5, 25, E_result));
            split_int(S_out, 21, 4, do_S(S6, 31, E_result));
            split_int(S_out, 25, 4, do_S(S7, 37, E_result));
            split_int(S_out, 29, 4, do_S(S8, 43, E_result));
            accumulate_bitstring("Sbox: ", S_out, 4);

            // do the P permutation
            permute(R, S_out, P_perm);
            accumulate_bitstring("P   :", R, 8);

            // xor this with old L to get the new R
            xor(R, temp_L);
            accumulate_bitstring("L[i]:", L, 8);
            accumulate_bitstring("R[i]:", R, 8);
        }

        // shift the CD values left 1 bit
        function shift_CD_1(CD) {
            var i;

            // note we use [0] to hold the bit shifted around the end
            for (i = 0; i <= 55; i++)
                CD[i] = CD[i + 1];

            // shift D bit around end
            CD[56] = CD[28];
            // shift C bit around end
            CD[28] = CD[0];
        }

        // shift the CD values left 2 bits
        function shift_CD_2(CD) {
            var i;
            var C1 = CD[1];

            // note we use [0] to hold the bit shifted around the end
            for (i = 0; i <= 54; i++)
                CD[i] = CD[i + 2];

            // shift D bits around end
            CD[55] = CD[27];
            CD[56] = CD[28];
            // shift C bits around end
            CD[27] = C1;
            CD[28] = CD[0];
        }


        // do the actual DES encryption/decryption
        function des_encrypt(inData, Key, do_encrypt) {
            var tempData = new Array(65); // output bits
            var CD = new Array(57); 	// halves of current key
            var KS = new Array(16); 	// per-round key schedules
            var L = new Array(33); 	// left half of current data
            var R = new Array(33); 	// right half of current data
            var result = new Array(65); // DES output
            var i;

            // do the initial key permutation
            permute(CD, Key, PC_1_perm);
            accumulate_bitstring("CD[0]: ", CD, 7);

            // create the subkeys
            for (i = 1; i <= 16; i++) {
                // create a new array for each round
                KS[i] = new Array(49);

                // how much should we shift C and D?
                if (i == 1 || i == 2 || i == 9 || i == 16)
                    shift_CD_1(CD);
                else
                    shift_CD_2(CD);
                accumulate_bitstring("CD[" + i + "]: ", CD, 7);

                // create the actual subkey
                permute(KS[i], CD, PC_2_perm);
                accumulate_bitstring("KS[" + i + "]: ", KS[i], 6);
            }

            // handle the initial permutation
            permute(tempData, inData, IP_perm);

            // split data into L/R parts
            for (i = 1; i <= 32; i++) {
                L[i] = tempData[i];
                R[i] = tempData[i + 32];
            }
            accumulate_bitstring("L[0]: ", L, 8);
            accumulate_bitstring("R[0]: ", R, 8);

            // encrypting or decrypting?
            if (do_encrypt) {
                // encrypting
                for (i = 1; i <= 16; i++) {
                    accumulate_output("Round " + i);
                    des_round(L, R, KS[i]);
                }
            }
            else {
                // decrypting
                for (i = 16; i >= 1; i--) {
                    accumulate_output("Round " + (17 - i));
                    des_round(L, R, KS[i]);
                }
            }

            // create the 64-bit preoutput block = R16/L16
            for (i = 1; i <= 32; i++) {
                // copy R bits into left half of block, L bits into right half
                tempData[i] = R[i];
                tempData[i + 32] = L[i];
            }
            accumulate_bitstring("LR[16] ", tempData, 8);

            // do final permutation and return result
            permute(result, tempData, FP_perm);
            return result;
        }
        // do encrytion/decryption
        // do_encrypt is TRUE for encrypt, FALSE for decrypt
        function do_des(do_encrypt) {


            var inData = new Array(65); // input message bits
            var Key = new Array(65);

            accumulated_output_info = "";

            // get the message from the user
            // also check if it is ASCII or hex
            get_value(inData, Data, false);

            // problems??
            if (inData[0] == ERROR_VAL) {
                //document.stuff.details.value = accumulated_output_info;
                return;
            }
            accumulate_bitstring("Input bits:", inData, 8);

            // get the key from the user
            get_value(Key, MyKey, false);

            // problems??
            if (Key[0] == ERROR_VAL) {
                //document.stuff.details.value = accumulated_output_info;
                return;
            }
            accumulate_bitstring("Key bits:", Key, 8);

            // do the encryption/decryption, put output in DES_output for display
            DES_output = des_encrypt(inData, Key, do_encrypt)

            accumulate_bitstring("Output ", DES_output, 8);
            // process output
            return format_DES_output();
            //document.stuff.details.value = accumulated_output_info;
        }

        // do Triple-DES encrytion/decryption
        // do_encrypt is TRUE for encrypt, FALSE for decrypt
        function do_tdes(do_encrypt) {
            var inData = new Array(65); // input message bits
            var tempdata = new Array(65); // interm result bits
            var KeyA = new Array(65);
            var KeyB = new Array(65);

            accumulated_output_info = "";

            // get the message from the user
            // also check if it is ASCII or hex
            get_value(inData, Data,
		document.stuff.intype[0].checked);

            // problems??
            if (inData[0] == ERROR_VAL) {
                //document.stuff.details.value = accumulated_output_info;
                return;
            }
            accumulate_bitstring("Input bits:", inData, 8);

            // get the key part A from the user
            get_value(KeyA, MyKey, false);
            // problems??
            if (KeyA[0] == ERROR_VAL) {
                //document.stuff.details.value = accumulated_output_info;
                return;
            }
            accumulate_bitstring("Key A bits:", KeyA, 8);


            // get the key part B from the user
            get_value(KeyB, document.stuff.keyb.value, false);
            // problems??
            if (KeyB[0] == ERROR_VAL) {
                //document.stuff.details.value = accumulated_output_info;
                return;
            }
            accumulate_bitstring("Key B bits:", KeyB, 8);

            if (do_encrypt) {
                // TDES encrypt = DES encrypt/decrypt/encrypt
                accumulate_output("---- Starting first encryption ----");
                tempdata = des_encrypt(inData, KeyA, true);
                accumulate_output("---- Starting second decryption ----");
                tempdata = des_encrypt(tempdata, KeyB, false);
                accumulate_output("---- Starting third encryption ----");
                DES_output = des_encrypt(tempdata, KeyA, true);
            }
            else {
                // TDES decrypt = DES decrypt/encrypt/decrypt
                accumulate_output("---- Starting first decryption ----");
                tempdata = des_encrypt(inData, KeyA, false);
                accumulate_output("---- Starting second encryption ----");
                tempdata = des_encrypt(tempdata, KeyB, true);
                accumulate_output("---- Starting third decryption ----");
                DES_output = des_encrypt(tempdata, KeyA, false);
            }

            accumulate_bitstring("Output ", DES_output, 8);

            // process output
            format_DES_output();
            //document.stuff.details.value = accumulated_output_info;
        }
    </script>
    <script type="text/javascript">
        var dscrc8_table = [
        0, 94, 188, 226, 97, 63, 221, 131, 194, 156, 126, 32, 163, 253, 31, 65,
      157, 195, 33, 127, 252, 162, 64, 30, 95, 1, 227, 189, 62, 96, 130, 220,
       35, 125, 159, 193, 66, 28, 254, 160, 225, 191, 93, 3, 128, 222, 60, 98,
      190, 224, 2, 92, 223, 129, 99, 61, 124, 34, 192, 158, 29, 67, 161, 255,
       70, 24, 250, 164, 39, 121, 155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
      219, 133, 103, 57, 186, 228, 6, 88, 25, 71, 165, 251, 120, 38, 196, 154,
      101, 59, 217, 135, 4, 90, 184, 230, 167, 249, 27, 69, 198, 152, 122, 36,
      248, 166, 68, 26, 153, 199, 37, 123, 58, 100, 134, 216, 91, 5, 231, 185,
      140, 210, 48, 110, 237, 179, 81, 15, 78, 16, 242, 172, 47, 113, 147, 205,
       17, 79, 173, 243, 112, 46, 204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
      175, 241, 19, 77, 206, 144, 114, 44, 109, 51, 209, 143, 12, 82, 176, 238,
       50, 108, 142, 208, 83, 13, 239, 177, 240, 174, 76, 18, 145, 207, 45, 115,
      202, 148, 118, 40, 171, 245, 23, 73, 8, 86, 180, 234, 105, 55, 213, 139,
       87, 9, 235, 181, 54, 104, 138, 212, 149, 203, 41, 119, 244, 170, 72, 22,
      233, 183, 85, 11, 136, 214, 52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
      116, 42, 200, 150, 21, 75, 169, 247, 182, 232, 10, 84, 215, 137, 107, 53];

        window.onload = function () {
            var ServerFlag = false;
            $.ajax({
                url: "SACPersonalisation.aspx/GetEmployeeDetails",
                type: "POST",
                dataType: "json",
                data: "{'EmployeeCode':'" + getParameterByName('EmpCode') + "'}",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    if (msg.d == "") {
                        alert("Internal Server Error. Please try again.");
                        ServerFlag = false;
                    }
                    else {
                        //alert(msg.d);
                        var xmlDoc = $.parseXML(msg.d);
                        var xml = $(xmlDoc);
                        var EmpDetails = xml.find("Table1");
                        document.getElementById("<%= txtEmployeeCode.ClientID %>").value = $(EmpDetails).find("EmpCode").text();
                        document.getElementById("<%= txtEmployeeName.ClientID %>").value = $(EmpDetails).find("EmpName").text();
                        document.getElementById("<%= txtDOB.ClientID %>").value = $(EmpDetails).find("DOB").text();
                        document.getElementById("<%= ddlGender.ClientID %>").value = $(EmpDetails).find("Gender").text();
                        document.getElementById("<%= txtCardExpiryDate.ClientID %>").value = $(EmpDetails).find("ExpiryDate").text();
                        document.getElementById("<%= txtCardExpiryTime.ClientID %>").value = "23:59";
                        ServerFlag = true;
                    }
                },
                error: function () { alert(arguments[2]); }
            });
            if (ServerFlag == false) {
                return false;
            }

            document.getElementById("<%= txtEmployeeCode.ClientID %>").value = getParameterByName('EmpCode');
        };


        var DosKeyPerso = "";
        var Data = "";
        var MyKey = "";
        var pin = "";

        function EmployeePerso() {
           
            try {
                var MasterKey = "";
                var DosKey = "";
                var ServerFlag = false;
                $.ajax({
                    url: "SACPersonalisation.aspx/GetMasterKey",
                    type: "POST",
                    dataType: "json",
                    data: "",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Master Key not found. Please Verify Master card first.");
                            ServerFlag = false;
                        }
                        else {
                            MasterKey = msg.d;
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (ServerFlag == false) {
                    return false;
                }

//                $.ajax({
//                    url: "SACPersonalisation.aspx/GetDOSKey",
//                    type: "POST",
//                    dataType: "json",
//                    data: "",
//                    async: false,
//                    contentType: "application/json; charset=utf-8",
//                    success: function (msg) {
//                        if (msg.d == "") {
//                            alert("Dos Key not found. Please Varify Dos card first.");
//                            ServerFlag = false;
//                        }
//                        else {
//                            DosKey = msg.d;
//                            ServerFlag = true;
//                        }
//                    },
//                    error: function () { alert(arguments[2]); }
//                });
//                if (ServerFlag == false) {
//                    return false;
//                }


                debugger;

                var objReadCard;
                var DataInitialize;
                var cardCSNR = "";
                objReadCard = new ActiveXObject("ContactlessCardRW.Card");
                DataInitialize = objReadCard.Initialise();
                if (DataInitialize != "") {
                    alert("Omnikey reader not connected or Error in card Initialization");
                    return false;
                }
                var data;
                data = objReadCard.ConnectToCard();
                if (data != "") {
                    alert("Error in connecting to card");
                    return false;
                }

                cardCSNR = objReadCard.CSNR.replace(/ /g, "").substring(0, 8);
                cardCSNR = ReverseCSNR(cardCSNR);
                var CSNRExist = false;
                $.ajax({
                    url: "SACPersonalisation.aspx/CheckCSNR",
                    type: "POST",
                    dataType: "json",
                    data: "{'CSNR':'" + cardCSNR + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "True") {
                            CSNRExist = true;
                        }
                        else {
                            alert("Card id does not exists.");
                            CSNRExist = false;
                            return false;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (CSNRExist == false) {
                    return false;
                }

                var CMSCalulatedKey = XOR("0000" + cardCSNR, "434D53434D53");
                debugger;
                //alert(CMSCalulatedKey);
                // ***********Sector 0 Start ******************************************************** //
                data = objReadCard.Authenticate(1, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 1");
                    return false;
                }
                WriteData = "";
                WriteData = "FF0F0400000000000248000000000000";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "1");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 1");
                    return false;
                }
                data = objReadCard.Authenticate(2, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 2");
                    return false;
                }
                WriteData = "";
                WriteData = "00000000000000000000010002000300";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "2");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 2");
                    return false;
                }

                data = objReadCard.Authenticate(3, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 3");
                    return false;
                }

                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 3");
                    return false;
                }
                // ***********Sector 0 End ******************************************************** //

                // ***********Sector 1 Start ******************************************************** //

                var EmpName = document.getElementById("<%= txtEmployeeName.ClientID %>").value.rpad(" ", 32);
                var WriteData = "";
                WriteData = EmpName.substr(0, 16);
                data = objReadCard.Authenticate(4, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 4");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "04");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 4");
                    return false;
                }
                WriteData = EmpName.substr(16, 32);
                data = objReadCard.Authenticate(5, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 5");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "05");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 5");
                    return false;
                }
                var DOB = document.getElementById("<%= txtDOB.ClientID %>").value;
                var DOE = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOB = DOB.replace(/\//g, "");
                DOE = DOE.replace(/\//g, "");
                ExpiryTime = ExpiryTime.replace(/:/g, "");
                var Gender = document.getElementById("<%= ddlGender.ClientID %>").value;
                if (Gender == "M") {
                    Gender = "01";
                }
                else {
                    Gender = "02";
                }
                WriteData = "";
                WriteData = DOB + Gender + "FFFFFFFFFFFFFFFFFFFFFF";
                //WriteData = DOB + Gender + DOE + ExpiryTime + "0000000000";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(6, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 6");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "6");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 6");
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(7, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 7");
                    return false;
                }
                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "7");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 7");
                    return false;
                }
                // *********** Sector 1 End******************************************************** //

                // ***********Sector 4 Start ******************************************************** //
                var EmployeeType = document.getElementById("<%= ddlEmployeeType.ClientID %>").value;
                if (EmployeeType == "P") {
                    EmployeeType = "01";
                }
                else {
                    EmployeeType = "02";
                }
                var EmployeeCode = document.getElementById("<%= txtEmployeeCode.ClientID %>").value;
                var EmployeeCodePrefix16 = EmployeeCode.substring(0, 2);
                var EmpCode16 = "0" + EmployeeCode.substring(2, EmployeeCode.length);
                WriteData = "";

             //   WriteData = ConvertStringToHex(EmployeeCodePrefix16 + EmpCode16) + "01" + "01010101" + "000000";
               pin = CreatePin(cardCSNR);
               var tempPin = "";
                for (var i = 0; i < pin.length; i++) {
                    tempPin = tempPin + "0" + pin.substr(i, 1).toString();
                   // //alert(tempPin);
               }
                WriteData = ConvertStringToHex(EmployeeCodePrefix16 + EmpCode16) + "01" + tempPin + "000000";
               alert(WriteData);
               WriteData = PadBytes(WriteData, 32);
                alert(WriteData);
               WriteData = objReadCard.Convert(WriteData);
               
            //   WriteData = objReadCard.Convert("00") + objReadCard.Convert(EmployeeCode) + objReadCard.Convert("00") + EmployeeType + objReadCard.Convert("00") + objReadCard.Convert("00000000000000");
               alert(WriteData);


               data = objReadCard.LoadKey("111111111111");
               if (data != "")
            {
                alert("Error in Load Key");
                return;
            }

            data = objReadCard.Authenticate(16, "111111111111", 96);

              //  data = objReadCard.Authenticate(16, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 16");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "10");
                alert(data);
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 4 Block 16");
                    return false;
                }

                var Key19 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR)
                WriteData = "";
                WriteData = PadBytes(Key19 + "78778869" + MasterKey, 32)
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(19, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 19");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "13");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 4 Block 19");
                    return false;
                }
                // ***********Sector 4 End ******************************************************** //

                // *********** Sector 10 Start ******************************************************** //
                var LocationCode = document.getElementById("<%= ddlLocationCode.ClientID %>").value;
                if (LocationCode == "S") {
                    LocationCode = "CB";
                }
                else if (LocationCode == "D") {
                    LocationCode = "CD";
                }
                else {
                    LocationCode = "CE";
                }
                var EmployeeType10 = document.getElementById("<%= ddlEmployeeType.ClientID %>").value;
                if (EmployeeType10 == "P") {
                    EmployeeType10 = "01";
                }
                else {
                    EmployeeType10 = "02";
                }
                var DOE10 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;

                DOE10 = DOE10.replace(/\//g, "");
                var date = DOE10.substring(0, 2);
                var month = DOE10.substring(2, 2);
                var year = DOE10.substring(6, 2);
                var EmployeeCode10 = document.getElementById("<%= txtEmployeeCode.ClientID %>").value;
                var EmployeeCodePrefix = EmployeeCode10.substring(0, 2);
                var EmpCode = "0" + EmployeeCode10.substring(2, EmployeeCode10.length);
                WriteData = "";
                WriteData = PadBytes("DD" + "B8" + LocationCode + EmployeeType10 + date + month + year + ConvertStringToHex(EmployeeCodePrefix) + EmpCode + "00000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                //WriteData = objReadCard.Convert("DD") + objReadCard.Convert("D6") + objReadCard.Convert(EmployeeType10) + objReadCard.Convert(DOE10) + objReadCard.Convert(EmployeeCode10) + objReadCard.Convert("00000000");
                data = objReadCard.Authenticate(40, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 40");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "28");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 10 Block 40");
                    return false;
                }
                WriteData = "";
                var DOS = DosKey;
                var master = MasterKey;
                WriteData = DOS.substring(2, (DOS.length - 2)) + "FF078069" + master;
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(43, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 43");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "2B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 10 Block 43");
                    return false;
                }
                // *********** Sector 10 Start ******************************************************** //
                // ***********************************Sector 13 Start ************************************************//
                var RandomNo13 = "738740688017";
                WriteData = RandomNo13;
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(52, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 52");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "34");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 13 Block 52");
                    return false;
                }
                WriteData = DOS.substring(2, (DOS.length - 2)) + "FF078069" + "FFFFFFFFFFFF";
                WriteData = PadBytes(WriteData, 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(55, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 55");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "37");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 13 Block 55");
                    return false;
                }
                // ***********************************Sector 13 End ************************************************//

                // ***********************************Sector 15 Start ************************************************//
                var CompanyCode15 = "0022";
                var SiteID15 = "01";
                var DOE15 = document.getElementById("<%= txtCardExpiryDate.ClientID %>").value;
                var ExpiryTime15 = document.getElementById("<%= txtCardExpiryTime.ClientID %>").value;
                DOE15 = DOE15.replace(/\//g, "");
                ExpiryTime15 = ExpiryTime15.replace(/:/g, "");
                var CheckSum15 = "78";
                WriteData = "";
                WriteData = PadBytes(CompanyCode15 + SiteID15 + "00" + "0002" + DOE15 + ExpiryTime15 + CheckSum15 + "0248" + "00", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.Authenticate(60, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 60");
                    return false;
                }
                data = objReadCard.WriteData(WriteData, "3C");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 60");
                    return false;
                }

                WriteData = "";
                data = objReadCard.Authenticate(63, "FFFFFFFFFFFF", 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 63");
                    return false;
                }
                WriteData = "";
                WriteData = CMSCalulatedKey + "FF078069" + "FFFFFFFFFFFF";
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3F");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 63");
                    return false;
                }
                // ***********************************Sector 15 End ************************************************//

                var empcode = document.getElementById("<%= txtEmployeeCode.ClientID %>").value

                $.ajax({
                    url: "SACPersonalisation.aspx/SendMail",
                    type: "POST",
                    dataType: "json",
                    data: "{'EmpCode':'" + empcode + "','pin':" + pin + ",'CSNR':'" + cardCSNR + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        //  alert(msg.d);
                        if (msg.d == "True") {

                            alert("Card personalised Successfully.");

                        }
                        else {
                            alert("Card is Personalised.");
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });

                window.location.href = "SACPersonalisationView.aspx";
                return false;
            }
            catch (ex) {
                alert(ex.Message);
                return false;
            }
        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function CreatePin(CSNR) {
            try {
                var Pin = "";

                var xor = XOR("01010101", CSNR);
                var array = [];
                var xorVal = xor;
                for (var i = 0; i < 4; i++) {
                    var rnd = xorVal.substring(0, 2);
                    xorVal = xorVal.substring(2, xorVal.length);
                    var val = parseInt(rnd, 16);
                    var tempVal = val.toString();
                    var oriVal = tempVal;
                    var Flag = 10;
                    while (Flag > 9) {
                        Flag = 0;
                        for (var j = 0; j < oriVal.length; j++) {
                            var digit = tempVal.substring(0, 1);
                            tempVal = tempVal.substring(1, tempVal.length);
                            Flag = Flag + parseInt(digit);
                        }
                        oriVal = Flag.toString();
                        tempVal = Flag.toString();

                    }
                    Pin = Pin + Flag;

                }
                return Pin;
            }
            catch (e) {
                alert(e);
            }
        }
        function CalculateCMSKeyWithRandomNo(RandomNo, CSNR) {
            try {
                var tempR = RandomNo;
                var tempC = CSNR;
                var CalculatedKey = "";
                for (var i = 0; i < 12; i = i + 2) {
                    var rnd = tempR.substring(0, 2);
                    tempR = tempR.substring(2, tempR.length);
                    var CRC = rnd;
                    for (var j = 0; j < 12; j = j + 2) {
                        var csn = tempC.substring(0, 2);
                        var CRC1 = "";
                        tempC = tempC.substring(2, tempC.length);


                        CRC1 = XOR(CRC, csn);
                        var temp = dscrc8_table[parseInt(CRC1, 16)];
                        CRC = temp.toString(16).toUpperCase();
                        if (CRC.length == 1) {
                            CRC = "0" + CRC;
                        }
                    }
                    tempC = CSNR;
                    CalculatedKey = CalculatedKey + CRC;

                }
                tempR = RandomNo;
                return CalculatedKey;
            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        function ReverseCSNR(csnr) {
            try {
                var temp = "";
                for (var i = 0; i < 4; i++) {
                    //temp = temp + csnr[csnr.length - 2] + csnr[csnr.length - 1];
                    temp = temp + csnr.substr(csnr.length - 2, 1) + csnr.substr(csnr.length - 1, 1);
                    csnr = csnr.substring(0, csnr.length - 2);
                }
                return temp;
            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        function PadBytes(val, num) {
            try {
                for (var i = val.length; i < num; i++) {
                    val = val + '0';
                }
                return val;
            }
            catch (ex) {
                alert(ex.Message);
            }
        }

        function XOR(val1, val2) {
            try {
                var result = "";
                if (val1.length == val2.length) {
                    for (var i = 0; i < val1.length; i++) {
                        var a = padZero(parseInt(val1.substr(i, 1), 16).toString(2));
                        var b = padZero(parseInt(val2.substr(i, 1), 16).toString(2));

                        //var a = padZero(parseInt(val1[i], 16).toString(2));
                        //var b = padZero(parseInt(val2[i], 16).toString(2));
                        var exor = "";
                        for (var j = 0; j < a.length; j++) {
                            exor += a.substr(j, 1) ^ b.substr(j, 1);
                        }
                        result += (parseInt(exor, 2).toString(16)).toUpperCase();
                    }
                }
                return result;
            }
            catch (ex) {
                alert(ex.Message + " XOR");
            }
        }

        function padZero(val) {
            try {
                for (var i = val.length; i < 4; i++) {
                    val = '0' + val;
                }
                return val;
            }
            catch (ex) {
                alert(ex.Message + " padZero");
            }
        }
        function ConvertStringToHex(strg) {
            try {
                var hex, i;
                var str = strg;
                var result = "";
                for (i = 0; i < str.length; i++) {
                    hex = str.charCodeAt(i).toString(16);
                    result += hex;
                }
                return result;
            }
            catch (ex) {
                alert(ex.Message + " ConvertStringToHex");
            }
        }

        //pads right
        String.prototype.rpad = function (padString, length) {
            var str = this;
            while (str.length < length)
                str = str + padString;
            return str;
        }

       

    </script>
    <style>
        #ContentPlaceHolder1_ContentPlaceHolder1_chkApplication td
        {
            text-align: left;
        }
        /*table
        {
        }
        td
        {
            color: Black;
            text-align: right;
        }*/
    </style>
    <!--[if IE]>
<style>
    .DivEmpDetails {
                     text-align: center;
            width: 95%;
            border: 1px solid #333333;
            border-radius: 15px;
            margin: 10px 10px 10px 10px;
            padding: 10px 10px 10px 10px;
            background-color:#53AEF3;
            font-family: 'Trebuchet MS' , Tahoma, Verdana, Arial, sans-serif;
    }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%; vertical-align: top; padding-top: 20px;">
                        <asp:Panel ID="pnlPersonalisation" runat="server" Style="width: 100%; text-align: center;">
                            <table style="margin: 0 auto;">
                                <tr>
                                    <td class="heading" colspan="3">
                                        Card Personalization
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee Code :"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <%--<asp:DropDownList ID="ddlEmployeeCode" runat="server" ValidationGroup="CardPerso"
                                onchange="__doPostBack('','Perso')" class="chosen-select">
                            </asp:DropDownList>--%>
                                        <asp:TextBox ID="txtEmployeeCode" runat="server" ValidationGroup="CardPerso" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td rowspan="10" style="vertical-align: top">
                                        <iframe frameborder="no" src="ImageUploadIframe.aspx" id="imgiFrame" style="border: none;
                                            height: 200px" scrolling="no" runat="server"></iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name :"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="TextControl" TabIndex="2"
                                            ReadOnly="true" ValidationGroup="CardPerso"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDOB" runat="server" Text="Date Of Birth :"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtDOB" runat="server" CssClass="TextControl" TabIndex="3" ReadOnly="true"
                                            ValidationGroup="CardPerso"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblGender" runat="server" Text="Gender :"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlGender" runat="server" AutoPostBack="true" CssClass="ComboControl"
                                            TabIndex="4" ReadOnly="true" Enabled="False" Width="120px" ValidationGroup="CardPerso">
                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCardExpiryDate" runat="server" Text="Expiry Date :"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtCardExpiryDate" runat="server" CssClass="TextControl" MaxLength="10"
                                            onkeydown="date_dash(this,event)" onkeypress="return IsNumber(event)" onkeyup="date_dash(this,event)"
                                            TabIndex="5" ValidationGroup="CardPerso"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="caltxtCardExpiryDate" runat="server" TargetControlID="txtCardExpiryDate"
                                            PopupButtonID="txtCardExpiryDate" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCardExpiryTime" runat="server" Text="Expiry Time :"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtCardExpiryTime" runat="server" CssClass="TextControl" MaxLength="5"
                                            onkeydown="Time_Colon(this,event)" onkeypress="return IsNumber(event)" onkeyup="Time_Colon(this,event)"
                                            TabIndex="2" ValidationGroup="CardPerso"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEmployeeType" runat="server" Text="Employee Type :"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="ComboControl" TabIndex="4"
                                            ReadOnly="true" Enabled="true" Width="120px" ValidationGroup="CardPerso">
                                            <asp:ListItem Value="P">Permanent</asp:ListItem>
                                            <asp:ListItem Value="T">Temporary</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLocationCode" runat="server" Text="Location Code :"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlLocationCode" runat="server" CssClass="ComboControl" TabIndex="4"
                                            ReadOnly="true" Enabled="true" Width="120px" ValidationGroup="CardPerso">
                                            <asp:ListItem Value="S">SAC</asp:ListItem>
                                            <asp:ListItem Value="D">DES</asp:ListItem>
                                            <asp:ListItem Value="B">BOPAL</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblApplication" runat="server" Text="Application :"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:CheckBoxList ID="chkApplication" runat="server" BorderColor="Gray" BorderWidth="1"
                                            TabIndex="3" Style="text-align: left;" ValidationGroup="CardPerso">
                                            <asp:ListItem> Time Attendance </asp:ListItem>
                                            <asp:ListItem> Access Control </asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center;">
                                        <asp:Button ID="btnSubmitPerso" runat="server" Text="Submit" CssClass="ButtonControl"
                                            OnClientClick="return EmployeePerso();" ValidationGroup="CardPerso" 
                                            />
                                            <%--onclick="btnSubmitPerso_Click"--%> 
                                        <asp:Button ID="btnCancelPerso" runat="server" Text="Cancel" CssClass="ButtonControl"
                                            OnClick="btnCancelPerso_Click" />
                                        <asp:HiddenField ID="hdnPerDate" runat="server" />
                                        <asp:HiddenField ID="hdnCardNum" runat="server" />
                                        <asp:HiddenField ID="hdnCSNR" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
