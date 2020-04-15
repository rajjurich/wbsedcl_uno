<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="SACRawCard.aspx.cs" Inherits="UNO.SACRawCard" %>

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




        var DosKeyPerso = "";
        var Data = "";
        var MyKey = "";




        function RawCard() {
            debugger;
            try {
                var MasterKey = "";
                var DosKey = "";
                var ServerFlag = false;
                $.ajax({
                    url: "SACRawCard.aspx/GetMasterKey",
                    type: "POST",
                    dataType: "json",
                    data: "",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        if (msg.d == "") {
                            alert("Master Key not found. Please Varify Master card first.");
                            ServerFlag = false;
                        }
                        else {
                            MasterKey = msg.d;
                            document.getElementById('hdnMasterKey').value = msg.d;
                            ServerFlag = true;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (ServerFlag == false) {
                    return false;
                }

                var objReadCard;
                var DataInitialize;
                var cardCSNR = "";
                var WriteData = "";
                DosKey = DosKey.substring(2, (DosKey.length - 2));
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
                //alert(cardCSNR);
                var CMSCalulatedKey = XOR("0000" + cardCSNR, "434D53434D53");

                /**************************************** Updating Database Start *********************************************/
                var DatabaseUpdate = false;
                $.ajax({
                    url: "SACRawCard.aspx/UpdateDatabase",
                    type: "POST",
                    dataType: "json",
                    data: "{'CSNR':'" + cardCSNR + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        //alert(msg.d);
                        if (msg.d == "True") {
                            DatabaseUpdate = true;
                        }
                        else {
                            alert("Database Error - Cannot Reinitialised card. Try again later");
                            DatabaseUpdate = false;
                        }
                    },
                    error: function () { alert(arguments[2]); }
                });
                if (DatabaseUpdate == false) {
                    return false;
                }

                /**************************************** Updating Database End *********************************************/
                /************************************** Erase Template Start ********************************************/
               /* var Sector = 16;
                for (var i = 0, j = 64; i < 4 && Sector < 28; i++, j++) {
                    if (i == 3) {
                        data = objReadCard.Authenticate(j, MasterKey, 96);
                        if (data != "") {
                            alert("Unable to Authenticate Block " + j);

                            $find("mpePersonaliseCard").hide();
                            return false;
                        }
                        var key = "FFFFFFFFFFFF" + "FF078069" + "FFFFFFFFFFFF";
                        key = objReadCard.Convert(key);
                        data = objReadCard.WriteData(key, parseInt(j, 10).toString(16).toUpperCase());
                        if (data != "") {
                            alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                            return false;
                        }
                        //alert("Key Sector " + Sector + " Block " + j + " : " + key);
                        i = 0 - 1;
                        Sector = Sector + 1;
                    }
                    else {
                        WriteData = "0".rpad("0", 32);
                        //alert("Data Sector " + Sector + " Block " + j + " : " + strWriteData);
                        data = objReadCard.Authenticate(j, MasterKey, 96);
                        if (data != "") {
                            alert("Unable to Authenticate Block " + j);

                            $find("mpePersonaliseCard").hide();
                            return false;
                        }
                        WriteData = objReadCard.Convert(WriteData);
                        data = objReadCard.WriteData(WriteData, parseInt(j, 10).toString(16).toUpperCase());
                        if (data != "") {
                            alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                            return false;
                        }
                    }
                }
                /************************************** Erase Template End ********************************************/

                /****************************************Sector 0 Start *********************************************/
                data = objReadCard.Authenticate(1, CMSCalulatedKey, 96);
                if (data != "") {
                    Reinitialize();
                    data = objReadCard.Authenticate(1, "FFFFFFFFFFFF", 96);
                    if (data != "") {
                         alert("Unable to Authenticate Block 1");
                         return false;
                     }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "01");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 1");
                    return false;
                }
                data = objReadCard.Authenticate(2, CMSCalulatedKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(2, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 2");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "02");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 02");
                    return false;
                }
                data = objReadCard.Authenticate(3, CMSCalulatedKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(3, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 3");
                      return false;
                  }
                }
                WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "03");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 03");
                    return false;
                }

                /****************************************Sector 0 End *********************************************/
                /****************************************Sector 1 Start *********************************************/
                data = objReadCard.Authenticate(4, CMSCalulatedKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(4, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 4");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "04");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 4");
                    return false;
                }
                data = objReadCard.Authenticate(5, CMSCalulatedKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(5, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 5");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "05");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 5");
                    return false;
                }
                data = objReadCard.Authenticate(6, CMSCalulatedKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(6, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 6");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "06");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 1 Block 6");
                    return false;
                }
                data = objReadCard.Authenticate(7, CMSCalulatedKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(7, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 7");
                      return false;
                  }
                }
                WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "07");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 0 Block 7");
                    return false;
                }
                /****************************************Sector 1 End *********************************************/

                
          /*  //****************Read Sec 14 block 0 for Time Attendance & Access Random No*********************

                //Authenticate sec 14 with Master Key
                  var Rnd =  [];
            Data = objReadCard.Authenticate(59, MasterKey, 96);
            if (Data != "")
            {
                Data = objReadCard.Authenticate(59, "FFFFFFFFFFFF", 96);

                if (Data != "")
                {
                    alert("Authentication Error Sector 14 Block 59");
                 
                    return false;
                }
            }
            else
            {
                var ReadData = objReadCard.ReadBlock(56);
                var Stemp = "";
               
                for (var i = 0; i < ReadData.length; i++)
                {
                    if (ReadData.substring(i, 1) != "") {

                        Stemp += ReadData.substring(i, 1);
                        alert(Stemp);
                    }
                }

                Stemp = objReadCard.Convert(Stemp);
                Rnd[3] = Stemp.substring(Stemp.length - 6, 6);
                Rnd[2] = Stemp.substring(4, 6);
             
            }
            //****************End*************************************************************************




               //**************************Raw Sector 2******************************************
                var RandomNo = Rnd[2];
                if (objReadCard.RndSecRaw(RandomNo, "08", "02") != true)
                {
                      WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "08");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 8");
                    return false;



                }
                }

            if (objReadCard.RndSecRaw(RandomNo, "08", "02") != true)
                {
                      WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "08");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 8");
                    return false;



                }
                }
            if (objReadCard.RndSecRaw(RandomNo, "09", "02") != true)
                {
                      WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "09");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 9");
                    return false;
                }



                }

            if (objReadCard.RndSecRaw(RandomNo, "10", "02") != true)
                {
                      WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0A");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 10");
                    return false;
                }

                }

            if (objReadCard.RndSecRaw(RandomNo, "11", "02") != true)
                {
                      WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 11");
                    return false;
                }
                }

              
            
            //****************************End************************************************


            



                /****************************************Sector 2 Start *********************************************/

                var Key2 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);
                //alert(Key2);






                data = objReadCard.Authenticate(11, Key2, 96);
                if (data != "") {
                    alert("Unable to Authenticate Block 11");
                    return false;
                }
                WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 11");
                    return false;
                }


                data = objReadCard.Authenticate(8, Key2, 96);
                if (data != "") {
                     data = objReadCard.Authenticate(8, "FFFFFFFFFFFF", 96);
                     if (data != "") {
                         alert("Unable to Authenticate Block 8");
                         return false;
                     }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "08");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 8");
                    return false;



                }
                data = objReadCard.Authenticate(9, Key2, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(9, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 9");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "09");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 9");
                    return false;
                }
                data = objReadCard.Authenticate(10, Key2, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(10, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 10");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0A");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 2 Block 10");
                    return false;
                }
                
                /****************************************Sector 2 End *********************************************/
                /****************************************Sector 3 Start *********************************************/
                var Key3 = CalculateCMSKeyWithRandomNo("738740688017", "0000" + cardCSNR);
                data = objReadCard.Authenticate(12, Key3, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(12, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 12");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0C");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 3 Block 12");
                    return false;
                }
                data = objReadCard.Authenticate(13, Key3, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(13, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 13");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0D");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 3 Block 13");
                    return false;
                }
                data = objReadCard.Authenticate(14, Key3, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(14, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 14");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0E");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 3 Block 14");
                    return false;
                }
                data = objReadCard.Authenticate(15, Key3, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(15, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 15");
                      return false;
                  }
                }
                WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "0F");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 3 Block 15");
                    return false;
                }
                /****************************************Sector 3 End *********************************************/
                /****************************************Sector 13 Start *********************************************/
                data = objReadCard.Authenticate(52, MasterKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(52, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 52 Please use correct mastercard");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "34");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 13 Block 52 Please use correct mastercard");
                    return false;
                }
                data = objReadCard.Authenticate(53, MasterKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(53, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 53");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "35");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 13 Block 53 Please use correct mastercard");
                    return false;
                }
                data = objReadCard.Authenticate(54, MasterKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(54, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 54");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "36");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 13 Block 54 Please use correct mastercard");
                    return false;
                }
                data = objReadCard.Authenticate(55, MasterKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(55, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 55");
                      return false;
                  }
                }
                WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "37");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 10 Block 55 Please use correct mastercard");
                    return false;
                }
                /****************************************Sector 13 End *********************************************/
                /****************************************Sector 14 Start *********************************************/
                data = objReadCard.Authenticate(56, MasterKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(56, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 56");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "38");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 56 Please use correct mastercard");
                    return false;
                }
                data = objReadCard.Authenticate(57, MasterKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(57, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 57");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "39");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 57 Please use correct mastercard");
                    return false;
                }
                data = objReadCard.Authenticate(58, MasterKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(58, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 58");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3A");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 58 Please use correct mastercard");
                    return false;
                }
                data = objReadCard.Authenticate(59, MasterKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(59, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 59");
                      return false;
                  }
                }
                WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3B");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 14 Block 59 Please use correct mastercard");
                    return false;
                }
                /****************************************Sector 14 End *********************************************/
                /****************************************Sector 15 Start *********************************************/
                data = objReadCard.Authenticate(60, CMSCalulatedKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(60, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 60");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3C");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 60");
                    return false;
                }
                data = objReadCard.Authenticate(61, CMSCalulatedKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(61, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 61");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3D");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 61");
                    return false;
                }
                data = objReadCard.Authenticate(62, CMSCalulatedKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(62, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 62");
                      return false;
                  }
                }
                WriteData = PadBytes("00000000000000000000000000000000", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3E");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 62");
                    return false;
                }
                data = objReadCard.Authenticate(63, CMSCalulatedKey, 96);
                if (data != "") {
                  data = objReadCard.Authenticate(63, "FFFFFFFFFFFF", 96);
                  if (data != "") {
                      alert("Unable to Authenticate Block 63");
                      return false;
                  }
                }
                WriteData = PadBytes("FFFFFFFFFFFFFF078069FFFFFFFFFFFF", 32);
                WriteData = objReadCard.Convert(WriteData);
                data = objReadCard.WriteData(WriteData, "3F");
                if (data != "") {
                    alert("Card Error: Unable to write in the card.Sector 15 Block 63");
                    return false;
                }
                /****************************************Sector 15 End *********************************************/
                /************************************** Erase Template Start ********************************************/
               /* var Sector = 16;
                for (var i = 0, j = 64; i < 4 && Sector < 28; i++, j++) {
                    if (i == 3) {
                        data = objReadCard.Authenticate(j, MasterKey, 96);
                        if (data != "") {
                            alert("Unable to Authenticate Block " + j);

                            $find("mpePersonaliseCard").hide();
                            return false;
                        }
                        var key = "FFFFFFFFFFFF" + "FF078069" + "FFFFFFFFFFFF";
                        key = objReadCard.Convert(key);
                        data = objReadCard.WriteData(key, parseInt(j, 10).toString(16).toUpperCase());
                        if (data != "") {
                            alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                            return false;
                        }
                        //alert("Key Sector " + Sector + " Block " + j + " : " + key);
                        i = 0 - 1;
                        Sector = Sector + 1;
                    }
                    else {
                        WriteData = "0".rpad("0", 32);
                        //alert("Data Sector " + Sector + " Block " + j + " : " + strWriteData);
                        data = objReadCard.Authenticate(j, MasterKey, 96);
                        if (data != "") {
                            alert("Unable to Authenticate Block " + j);

                            $find("mpePersonaliseCard").hide();
                            return false;
                        }
                        WriteData = objReadCard.Convert(WriteData);
                        data = objReadCard.WriteData(WriteData, parseInt(j, 10).toString(16).toUpperCase());
                        if (data != "") {
                            alert("Card Error: Unable to write in the card.Sector " + Sector + " Block " + j);
                            return false;
                        }
                    }
                }
                /************************************** Erase Template End ********************************************/
                alert("Card Reinitialised");
                return false;
            }
            catch (ex) {
                alert(ex.Message);
                return false;
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


        function Reinitialize() {
            try {
                var obj_Card;
                obj_Card = new ActiveXObject("ContactlessCardRW.Card");

                var Data = "";
                var Rnd = new Array();

                var DataInitialize;
                DataInitialize = obj_Card.Initialise();
                if (DataInitialize != "") {
                    alert("Omnikey reader not connected or Error in card Initialization");
                    return false;
                }

                Data = obj_Card.ConnectToCard();
                if (Data != "") {
                    alert("Error in connecting to card");
                    return false;
                }

                //*****************Read User Card Company Code & site id & Match with Master Card************

                var MasterKey = document.getElementById('hdnMasterKey').value
//                var MasterDetails = document.getElementById('hdnCompSiteCd').value
//                var MasterCompCd = MasterDetails.substr(0, 4);
//                var MasterSiteId = MasterDetails.substr(4, 3);

//                var UserCardDetails = "";
//                UserCardDetails = obj_Card.ReadCMP(document.getElementById('hdnUserId').value, document.getElementById('hdnPassword').value);
//                var UserCompCd = UserCardDetails.substr(0, 4);
//                var UserSiteId = UserCardDetails.substr(4, 3);


//                if (MasterCompCd != UserCompCd && MasterSiteId != UserSiteId) {
//                    alert("Company code or site id does not match.Please Contact CMS Computers LTD");
//                    return false;
//                }
                //                 else {
                //                     alert("Company code or site id match successfully");
                //                 }

                //*********end of reading User Card***************************************************************

                //*****************Read App Code from sec 0********************************************************
                Data = obj_Card.Authenticate(3, obj_Card.Cardrawkeybuf, 96);
                if (Data != "") {
                    alert("Authentication Error Sector 0 Block 3");
                    return false;
                }
                else {
                    var OldDataBlock1 = obj_Card.ReadBlock(1);
                    var OldDataBlock2 = obj_Card.ReadBlock(2);
                    // alert(OldDataBlock1);
                }
                //****************end of reading app code***********************************************************

                //****************Read Sec 14 block 0 for Time Attendance & Access Random No*********************

                //Authenticate sec 14 with Master Key
                Data = obj_Card.Authenticate(59, MasterKey, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(59, "FFFFFFFFFFFF", 96);

                    if (Data != "") {
                        alert("Authentication Error Sector 14 Block 59");
                        return false;
                    }
                }
                else {
                    var ReadData = obj_Card.ReadBlock(56);
                    var Stemp = "";

                    for (var i = 0; i < ReadData.length; i++) {
                        if (ReadData.substring(i, 1) != " ") {
                            Stemp += ReadData.substring(i, 1);
                        }
                    }
                    //  alert(Stemp);
                    Stemp = obj_Card.Convert(Stemp);
                    Rnd[3] = Stemp.substring(Stemp.length - 6, 6);
                    Rnd[2] = Stemp.substring(4, 6);

                }
                //****************End*************************************************************************
                //***********below code for making sector 0 block 1,2 raw*********************************
                Data = obj_Card.Authenticate(2, obj_Card.Cardrawkeybuf, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(2, "FFFFFFFFFFFF", 96);

                    if (Data != "") {
                        alert("Authentication Error Sector 0 Block 2");
                        return false;
                    }
                }
                else {
                    var WriteData = "00000000000000000000020002000200";
                    Data = obj_Card.WriteBlock(2, WriteData);
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 0 Block 2");
                        return false;
                    }
                    // alert("Making Sector Raw.Sector 0 Block 2" + Data);

                    var WriteData1 = "FF0F0000000000000000000000000000";
                    Data = obj_Card.WriteBlock(1, WriteData1);
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 0 Block 1");
                        return false;
                    }
                    //  alert("Making Sector Raw.Sector 0 Block 1" + Data);
                }
                //**********End***************************************************************
                //***********below code for making sector 1 raw*********************************

                Data = obj_Card.Authenticate(6, obj_Card.Cardrawkeybuf, 96);
                if (Data != "") {
                    alert("Authentication Error Sector 1 Block 6");
                    return false;
                }
                else {
                    var WriteData = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
                    if (obj_Card.WriteBlock(6, WriteData) != "") {
                        alert("Error in writting data to Sector 1 Block 6");
                        return false;
                    }
                }
                //  alert("writting data to Sector 1 Block 6" + Data);

                Data = obj_Card.Authenticate(7, obj_Card.Cardrawkeybuf, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(7, "FFFFFFFFFFFF", 96);

                    if (Data != "") {
                        alert("Authentication Error Sector 1 Block 7");
                        return false;
                    }
                }
                else {
                    Data = obj_Card.MakeSectorRaw("7", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 1 Block 7");
                        return false;
                    }
                    //     alert("Making Sector Raw.Sector 1 Block 7" + Data);
                }

                //**********************End************************************************    

                //**************************Raw Sector 2******************************************
                var RandomNo = Rnd[2];
                if (obj_Card.RndSecRaw(RandomNo, "08", "02") != true) {
                    alert("Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 8");
                    return false;
                }

                if (obj_Card.RndSecRaw(RandomNo, "11", "02") != true) {
                    alert("Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 11");
                    return false;
                }

                //*************************Raw Sector 3************************************************
                var RandomNo1 = Rnd[3];
                if (obj_Card.RndSecRaw(RandomNo1, "12", "03") != true) {
                    alert("Making Sector Raw for Time Attendance. Card Error, Sector 2 Block 8");
                    return false;
                }
                if (obj_Card.RndSecRaw(RandomNo1, "15", "03") != true) {
                    alert("Making Sector Raw for Access Control.Card Error,Sector 3 Block 15");
                    return false;
                }

                //****************************End************************************************

                //***********below code for making sector 13 raw*********************************
                Data = obj_Card.Authenticate(55, MasterKey, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(55, "FFFFFFFFFFFF", 96);
                    if (Data != "") {
                        alert("Authentication Error Sector 13 Block 37");
                        return false;
                    }
                }
                else {
                    Data = obj_Card.MakeSectorRaw("37", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 13 Block 55");
                        return false;
                    }
                    //   alert("Making Sector Raw.Sector 13 Block 55" + Data);
                }

                //**********************End************************************************

                //***********below code for making sector 14 raw*********************************
                Data = obj_Card.Authenticate(59, MasterKey, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(59, "FFFFFFFFFFFF", 96);

                    if (Data != "") {
                        alert("Authentication Error Sector 14 Block 59");
                        return false;
                    }
                }
                else {
                    Data = obj_Card.MakeSectorRaw("3B", "FFFFFFFFFFFF", "FFFFFFFFFFFF");
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 14 Block 59");
                        return false;
                    }
                    // alert("Making Sector Raw.Sector 14 Block 59" + Data);
                }

                //**********************End************************************************          

                //**********************Raw Sector 15 block 60********************************
                Data = obj_Card.Authenticate(63, obj_Card.Cardrawkeybuf, 96);
                if (Data != "") {
                    Data = obj_Card.Authenticate(2, "FFFFFFFFFFFF", 96);

                    if (Data != "") {
                        alert("Authentication Error Sector 15 Block 60");
                        return false;
                    }
                }
                else {
                    var ReadData = obj_Card.ReadBlock(60);
                    var Stemp = "";

                    for (var i = 0; i < ReadData.length; i++) {
                        if (ReadData.substr(i, 1) != " ") {
                            Stemp += ReadData.substr(i, 1);
                        }
                    }

                    ReadData = Stemp;

                    var Writedata = "000000000000000000000000000000" + ReadData.substr(ReadData.length - 2, 2);
                    // alert(Writedata);
                    Data = obj_Card.WriteBlock(60, Writedata);
                    if (Data != "") {
                        alert("Making Sector Raw.Card Error,Sector 15 Block 60");
                        return false;
                    }
                    //  alert("Making Sector Raw.Sector 15 Block 60" + Data);
                }

                alert("Card Reinitialized Successfully.");
                return true;
                //*************end************************************************************

            }
            catch (e) {
                alert(e.message);
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlPersonalisation" runat="server" Style="width: 100%; text-align: center;">
        <table style="margin: 0 auto;">
            <tr>
                <td colspan="2" class="heading">
                    Please place card to reinitialise/disable and press OK
                </td>
            </tr>
             
            <tr>
             

                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnSubmitPerso" runat="server" Text="OK" CssClass="ButtonControl"
                        OnClientClick="return RawCard();" ValidationGroup="CardPerso" 
                         />
                    <asp:Button ID="btnCancelPerso" runat="server" Text="Cancel" CssClass="ButtonControl"
                        OnClientClick="return false;" Style="display: none;" />
                </td>
               
            </tr>
             
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hdnMasterKey" ClientIDMode="Static" runat="server"/>   
</asp:Content>
