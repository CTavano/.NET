//////////////////////////////////////////////////////////////////////////////////////
///CTavano - Aug 2020
///Giganteger:
///Creating a program that bypasses the limited range constraints of unsigned integers
//////////////////////////////////////////////////////////////////////////////////////
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace CTavano_Giganteger2020{
    public class GInt : IComparable{
        private readonly List<byte> Giganteger = new List<byte>();

        //CTOR for default state
        public GInt() => Giganteger.Add(0);

        //CTOR for a number being input up to a value of UInt64 and converted to a list of bytes
        public GInt(UInt64 number){
            //if the number is 0, then insert a 0
            if (number == 0) Giganteger.Insert(0, (byte)number);
            //otherwise populate the list with the number
            while (number > 0){
                UInt64 temp = number % 10;
                Giganteger.Insert(0, (byte)temp);
                number /= 10;
            }
        }

        //CTOR for a number being input as a string and converted to a list of bytes.
        public GInt(string String){
            if (String == "") Giganteger.Add(0);

            foreach (char c in String){
                byte tempByte = Convert.ToByte(c);
                tempByte -= 48;
                //if the value of that char is between 0 and 9 we can add it to the list
                if ((tempByte <= 9) && (tempByte >= 0)) Giganteger.Add(tempByte);
                //otherwise throw and error
                else throw new Exception("Not a real number");
            }
        }

        //CTOR for copying an existing list of bytes.
        public GInt(GInt gInt) => gInt.Giganteger.ForEach(b => Giganteger.Add(b));

        //Equals override to compare GInt numbers
        public override bool Equals(object obj){
            //check to see if they are of the same data type
            if (!(obj is GInt arg)) return false;
            //check to see if they are the same size
            if (Giganteger.Count != arg.Giganteger.Count) return false;
            //check to see if all the digits are the same
            for (int a = 0; a < Giganteger.Count; a++){
                if (Giganteger[a] != arg.Giganteger[a]) return false;
            }
            return true;
        }

        //Override GetHashCode
        public override int GetHashCode() => 1;

        //CompareTo for sorting a list of numbers
        public int CompareTo(object obj){
            int result = 0;
            if (!(obj is GInt arg)) throw new Exception("Not of type GInt!");
            //if the list.count isn't the same then return an answer
            if (Giganteger.Count != arg.Giganteger.Count) return Giganteger.Count < arg.Giganteger.Count ? -1 : 1;
            //if the list.count is the same, then check the digits in each list
            if (Giganteger.Count == arg.Giganteger.Count){
                for (int a = Giganteger.Count-1; a >= 0; a--){
                    if (Giganteger[a] < arg.Giganteger[a]) result = -1;
                    if (Giganteger[a] > arg.Giganteger[a]) result = 1;
                }
            }
            return result;
        }

        //override ToString()
        public override string ToString(){
            //return BitConverter.ToString(Giganteger.ToArray());
            string temp = "";
            //change to lambda for space
            for (int i = 0; i < Giganteger.Count; i++){
                temp += (Giganteger[i]).ToString();
            }
            return temp;
        }

        /// <summary>
        /// Adding method that leverages the actual adding method
        /// </summary>
        /// <param name="numTwo"></param>
        /// <returns></returns>
        public GInt Add(GInt numTwo) => Add(this,numTwo);

        /// <summary>
        /// Adding method for adding two numbers together
        /// </summary>
        /// <param name="numOne">number one</param>
        /// <param name="numTwo">number two</param>
        /// <returns></returns>
        public static GInt Add(GInt numOne, GInt numTwo) {
            GInt tempNumOne = new GInt(numOne); //make a copy of the first number we are manipulating
            GInt tempNumTwo = new GInt(numTwo); //make a copy of the second number we are manipulating
            GInt answer = new GInt();           //used to hold the answer
            bool carryFlag = false;             //used if a carry is needed
            int tempAnswer = 0;                 //used to hold the single digit adding
            int digCount = 0;                   //used to hold the number of digits of the larger number
            int digitA = 0;                     //digit A
            int digitB = 0;                     //digit B

            //find the larger number
            if (numOne.CompareTo(numTwo) == 1) digCount = numOne.Giganteger.Count;
            else if (numOne.CompareTo(numTwo) == -1) digCount = numTwo.Giganteger.Count;
            else digCount = numOne.Giganteger.Count;

            //reverse both numbers for adding
            tempNumOne.Giganteger.Reverse();
            tempNumTwo.Giganteger.Reverse();

            //if the lists only contains 1 digit
            if (digCount == 0){
                tempAnswer = numOne.Giganteger[0] + numTwo.Giganteger[0];
                //if both the digits added together are less than 10
                if (tempAnswer < 10) answer.Giganteger[0] = (byte)tempAnswer;
                //if both the digits added together are more than 9
                if (tempAnswer > 9) answer.Giganteger.Insert(0, (byte)1);
            }

            //if the lists contains more than one digit
            if (digCount > 0){
                for (int a = 0; a < digCount; a++){
                    //get the number that is in that part of the list,
                    //if we are trying to get a number that isn't in the list, set it to zero
                    digitA = tempNumOne.Giganteger.Count - 1 < a ? 0 : tempNumOne.Giganteger[a];
                    digitB = tempNumTwo.Giganteger.Count - 1 < a ? 0 : tempNumTwo.Giganteger[a];

                    //add the two numbers together
                    tempAnswer = digitA + digitB;
                    //add 1 to the digit if the previous number was over 10 to simulate a carry
                    if (carryFlag) tempAnswer += 1;
                    //set the Carry flag according to the tempAnswer result
                    carryFlag = tempAnswer >= 10 ? true : false;
                    //if the Carry flag is set, then only add the last digit and save the carry
                    if (carryFlag) tempAnswer %= 10;
                    //if its the first digit then just change the value
                    if (a == 0) answer.Giganteger[0] = (byte)tempAnswer;
                    //insert the digit into the list
                    else answer.Giganteger.Insert(0, (byte)tempAnswer);
                }
                //if there are no more numbers to add and the carry flag is still true, then add a 1 to the front of the list
                if (carryFlag) answer.Giganteger.Insert(0, (byte)1);
            }
            return answer;
        }

        /// <summary>
        /// Subtracting method that leverages the actual subtracting method
        /// </summary>
        /// <param name="numTwo"></param>
        /// <returns></returns>
        public GInt Sub(GInt numTwo) => Sub(this,numTwo);

        /// <summary>
        /// Subtracting method
        /// </summary>
        /// <param name="numOne">Number one to subtract</param>
        /// <param name="numTwo">Number two to subtract</param>
        /// <returns></returns>
        public static GInt Sub(GInt numOne, GInt numTwo){
            GInt tempNumOne = new GInt(numOne); //make a copy of the first number we are manipulating
            GInt tempNumTwo = new GInt(numTwo); //make a copy of the second number we are manipulating
            GInt answer = new GInt();           //used to hold the answer
            bool borrowFlag = false;            //used if a carry is needed
            int tempAnswer = 0;                 //used to hold the single digit adding
            int digCount = 0;                   //used to hold the number of digits of the larger number
            int digitA = 0;                     //digit A
            int digitB = 0;                     //digit B

            //find the larger number
            if (numOne.CompareTo(numTwo) == 1) digCount = numOne.Giganteger.Count;
            else if (numOne.CompareTo(numTwo) == -1) throw new Exception("You can't subtract those numbers");
            else digCount = numOne.Giganteger.Count;

            //reverse both numbers for adding
            tempNumOne.Giganteger.Reverse();
            tempNumTwo.Giganteger.Reverse();

            //if the lists only contains 1 digit
            if (digCount == 0){
                tempAnswer = numOne.Giganteger[0] - numTwo.Giganteger[0];
                //if both the digits subtracted are less that 0
                if (tempAnswer < 0) throw new Exception("You can't subtract those numbers");
                //if both the digits subtracted are greater than 0
                if (tempAnswer > 0) answer.Giganteger[0] = (byte)tempAnswer;
            }

            //if there is more than one digit
            if (digCount > 0){
                for (int a = 0; a < digCount; a++){
                    //get the number that is in that part of the list,
                    //if we are trying to get a number that isn't in the list, set it to zero
                    digitA = tempNumOne.Giganteger.Count - 1 < a ? 0 : tempNumOne.Giganteger[a];
                    digitB = tempNumTwo.Giganteger.Count - 1 < a ? 0 : tempNumTwo.Giganteger[a];

                    //remove one from the digit if the borrow flag is set
                    if (borrowFlag) digitA -= 1;
                    borrowFlag = false;

                    //if we subtract one of digitA and it was a 0, then we need to set it to a 9 and move on
                    //keep the borrow flag set to true because we need to keep borrowing.
                    if (digitA > 9 && tempNumOne.Giganteger.Count > a){
                        borrowFlag = true;
                        digitA = 9;
                    }

                    //if the digitA is greater than digitB then subtract the two numbers
                    if (digitA > digitB) tempAnswer = digitA - digitB;
                    //if the digits are the same, just make the answer 0
                    else if (digitA == digitB) tempAnswer = 0;
                    //if the digitA is less than digitB then add 10 to it and set the borrow flag
                    else if (digitA < digitB){
                        digitA += 10;
                        borrowFlag = true;
                    }

                    //subtract the two numbers
                    tempAnswer = digitA - digitB;
                    //if its the first digit then just change the value
                    if (a == 0) answer.Giganteger[0] = (byte)tempAnswer;
                    //otherwise if its a 0 and we are at the end of the list, just ignore it
                    else if (tempAnswer == 0 && a == digCount - 1) { }
                    //or just place it into the list
                    else answer.Giganteger.Insert(0, (byte)tempAnswer);
                }

                //kill all leading zeros that don't matter
                answer.Giganteger.Reverse();                
                for (int i = answer.Giganteger.Count - 1; i > 0; i--){
                    if (answer.Giganteger[i] != 0) break;
                    answer.Giganteger.RemoveAt(i);
                }
                answer.Giganteger.Reverse();
            }

            return answer;
        }


        /// <summary>
        /// SMult method that leverages the actual SMult method
        /// </summary>
        /// <param name="numbTwo">Number to multiply</param>
        /// <returns></returns>
        public GInt SMult(GInt numbTwo) => SMult(this, numbTwo);

        /// <summary>
        /// Actual SMult method for multiplying 2 numbers together
        /// </summary>
        /// <param name="numOne">First number</param>
        /// <param name="numTwo">Second number</param>
        /// <returns></returns>
        public static GInt SMult(GInt numOne, GInt numTwo){
            GInt sub1 = new GInt("1");  //temp number representing the number 1
            GInt total = new GInt();    //number to hold the total number
            GInt temp = new GInt();     //temp number for holding a number

            //if either of the two numbers are 0, then we don't need to do anything.
            if ((numOne.CompareTo(temp) == 0) || numTwo.CompareTo(temp) == 0) return temp;

            //otherwise check to see what number is bigger before multiplying
            if (numOne.CompareTo(numTwo) == -1){
                while (!numOne.Equals(temp)){
                    total = total.Add(numTwo);
                    numOne = numOne.Sub(sub1);
                }
            }

            else{
                while (!numTwo.Equals(temp)){
                    total = total.Add(numOne);
                    numTwo = numTwo.Sub(sub1);
                }
            }

            return total;
        }

        /// <summary>
        /// IDiv method that leverages dividing numbers
        /// </summary>
        /// <param name="numTwo">Second number</param>
        /// <returns></returns>
        public GInt IDiv(GInt numTwo) => IDiv(this, numTwo);

        /// <summary>
        /// IDiv method for dividing 2 numbers
        /// </summary>
        /// <param name="numOne">First number</param>
        /// <param name="numTwo">Second number</param>
        /// <returns></returns>
        public static GInt IDiv(GInt numOne, GInt numTwo){
            GInt tempNumOne = new GInt(numOne); //number for holding a copy of the first number
            GInt total = new GInt();            //number for holding the total
            GInt one = new GInt(1);             //the number 1

            //if the number we are going to divide is 0, just return 0
            if (numOne.CompareTo(new GInt()) == 0) return total;
            //if the divisor is a 0 throw and error
            else if (numTwo.CompareTo(new GInt()) == 0) throw new Exception("Can't divide by zero!");
            //if the first number is smaller than the divisor, then throw and error
            else if (numOne.Giganteger.Count() < numTwo.Giganteger.Count()) return total;

            //keep subtracting the divisor from the first number until you can no longer subtract
            while (tempNumOne.CompareTo(numTwo) != -1) {
               tempNumOne = tempNumOne.Sub(numTwo);
               total = total.Add(one);
            }

            return total;
        }

        /// <summary>
        /// FMult method that leverages the FMult method
        /// </summary>
        /// <param name="numTwo">the divisor</param>
        /// <returns></returns>
        public GInt FMult(GInt numTwo) => FMult(this, numTwo);

        /// <summary>
        /// FMult method for dividing 2 numbers
        /// </summary>
        /// <param name="numOne">First number</param>
        /// <param name="numTwo">Second number</param>
        /// <returns></returns>
        public static GInt FMult(GInt numOne, GInt numTwo) {
            GInt tempNumOne = new GInt(numOne); //make a copy of the first number we are manipulating
            GInt tempNumTwo = new GInt(numTwo); //make a copy of the second number we are manipulating
            GInt total = new GInt();            //number to hold the total number
            GInt temp = new GInt();             //number to hold a temp value
            GInt tempNum1;                      //hold the temp value of number 1
            GInt tempNum2;                      //hold the temp value of number 2


            //if either of the two numbers are 0, then we don't need to do anything.
            if ((numOne.CompareTo(temp) == 0) || numTwo.CompareTo(temp) == 0) return temp;

            //reverse the numbers
            tempNumOne.Giganteger.Reverse();
            tempNumTwo.Giganteger.Reverse();

            //otherwise check to see what number is bigger before multiplying
            if (numOne.CompareTo(numTwo) == -1){
                //multiply each number of one number by each number of the other number
                for (int i = 0; i < numTwo.Giganteger.Count; i++){
                    for (int j = 0; j < numOne.Giganteger.Count; j++){
                        tempNum1 = new GInt(tempNumOne.Giganteger[j]);
                        tempNum2 = new GInt(tempNumTwo.Giganteger[i]);
                        temp = tempNum1.SMult(tempNum2);
                        //add zeros based on position of numbers
                        for (int z = 0; z < (j + i); z++){
                            temp.Giganteger.Add((byte)0);
                        }

                        total = total.Add(temp);
                    }
                }
            }

            else{
                for (int i = 0; i < numOne.Giganteger.Count; i++){
                    for (int j = 0; j < numTwo.Giganteger.Count; j++){
                        tempNum1 = new GInt(tempNumOne.Giganteger[i]);
                        tempNum2 = new GInt(tempNumTwo.Giganteger[j]);
                        temp = tempNum1.SMult(tempNum2);

                        for (int z = 0; z < (j + i); z++){
                            temp.Giganteger.Add((byte)0);
                        }

                        total = total.Add(temp);
                    }
                }
            }

            return total;
        }
    }
}