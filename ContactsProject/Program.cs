using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Speech.Synthesis;

namespace ContactsProject {
    class Program {

        struct Contacts {
            public string FirstName;
            public string LastName;
            public string PhoneNumber;
            public string Email;
        }

       

        static void Main(string[] args) {
            setupConsole(ConsoleColor.Gray, ConsoleColor.DarkBlue);
            bool RunProgram = true;
            string SearchNameVar = "";
            int userInput = 0;
            string findfile = ("C:\\temp\\Contacts.cnt");
            while (RunProgram) {
                doubleSpeak("This a program designed to hold contacts information. Please Select an option (0-7)");
                Console.WriteLine("This a program designed to hold contacts information. Please Select an option (0-7)");
                Console.WriteLine("####################################");
                Console.WriteLine("0. Add a new record");
                Console.WriteLine("1. Search for a name");
                Console.WriteLine("2. Modify a phone number");
                Console.WriteLine("3. Modify an email");
                Console.WriteLine("4. Delete a record");
                Console.WriteLine("5. List all contacts");
                Console.WriteLine("6. Alphabetize contacts");
                Console.WriteLine("7. Exit the program");

                userInput = int.Parse(prompt("What would you like to do?"));
                Console.WriteLine("You have pressed " + userInput);

                //user input validation 
                if (userInput < 0 && userInput > 7) {
                    Console.WriteLine("You have pressed an incorrect button, please try again.");
                    userInput = int.Parse(prompt("Please re-enter a number 0-7"));
                    Console.WriteLine("You have pressed " + userInput);

                }

                else if (userInput == 0) {
                    AddNewRecord(findfile);
                    Console.ReadKey();
                    Console.Clear();

                } else if (userInput == 1) {
                    SearchNameVar = prompt("What name would you like to search for?");
                    SearchName(findfile, SearchNameVar);
                    Console.ReadKey();
                    Console.Clear();

                } else if (userInput == 2) {
                    ModifyPhoneNumber(findfile, CountFile(findfile));
                    Console.ReadKey();
                    Console.Clear();

                } else if (userInput == 3) {
                    ModifyEmail(findfile, CountFile(findfile));
                    Console.ReadKey();
                    Console.Clear();

                } else if (userInput == 4) {
                    DeleteContact(findfile, CountFile(findfile));
                    Console.ReadKey();
                    Console.Clear();

                } else if (userInput == 5) {
                    ListContacts(findfile);
                    Console.ReadKey();
                    Console.Clear();

                } else if (userInput == 6) {
                    Alphabetize(findfile, CountFile(findfile));
                    Console.ReadKey();
                    Console.Clear();

                } else if (userInput == 7) {
                    simpleSpeak("Goodbye");
                    Console.WriteLine("Goodbye");
                    Console.ReadKey();
                    Console.Clear();
                    
                    RunProgram = false;
                }

            }
            }//END MAIN

            static void get_file_fromPath(string tempPath, Contacts[] ary) {
                string line = "";
                int counter = 0;
                StreamReader in_file = new StreamReader(tempPath);

                while (!in_file.EndOfStream) {
                    line = in_file.ReadLine();
                    string[] tempAry = line.Split(',');
                    ary[counter].FirstName = tempAry[0];
                    ary[counter].LastName = tempAry[1];
                    ary[counter].PhoneNumber = tempAry[2];
                    ary[counter].Email = tempAry[3];
                    counter++;
                }
                in_file.Close();
            }

            static void PrintArray(Contacts[] temp) {
                for (int i = 0; i < temp.GetLength(0); i += 1) {
                    Console.Write(temp[i].FirstName + ", ");
                    Console.Write(temp[i].LastName + ", ");
                    Console.Write(temp[i].PhoneNumber + ", ");
                    Console.WriteLine(temp[i].Email);

                }
                Console.Write(" ");
            }

            static void ListContacts(string tempPath) {
                string Line = "";
                StreamReader in_file = new StreamReader(tempPath);
                while (!in_file.EndOfStream) {
                    Line = in_file.ReadLine();
                    Console.WriteLine(Line);
                doubleSpeak(Line);
                }
                in_file.Close();
            }

            static void simpleSpeak(string words) {
          
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.SelectVoiceByHints(VoiceGender.Female);
                synthesizer.Volume = 100;  //0....100
                synthesizer.Rate = -2;
                synthesizer.Speak(words);
                
            }

        static void doubleSpeak(string words) {
          
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.SelectVoiceByHints(VoiceGender.Female);
                synthesizer.Volume = 100;  //0....100
                synthesizer.Rate = -2;
                synthesizer.SpeakAsync(words);
                
            }

            static void DeleteContact(string tempPath, int count) {
               
                StreamReader in_file = new StreamReader(tempPath);
                int counter = 0;
                Contacts[] contactData = new Contacts[count];
                string userFN = prompt("Type the First name of the contact you would like to delete");
                string userLN = prompt("Type the Last name of the contact you would like to delete");
                string[] contacts = new string[count];
                while (!in_file.EndOfStream) {

                    string data = in_file.ReadLine();
                    contacts = data.Split(',');

                    contactData[counter].FirstName = contacts[0];
                    contactData[counter].LastName = contacts[1];
                    contactData[counter].PhoneNumber = contacts[2];
                    contactData[counter].Email = contacts[3];
                 counter++;
               
                }//END WHILE
            in_file.Close();
            StreamWriter out_file = new StreamWriter(tempPath);
               for (int i = 0; i < contactData.Length; i++) {
                if (userFN != contactData[i].FirstName || userLN != contactData[i].LastName) {
                    out_file.WriteLine(contactData[i].FirstName + "," + contactData[i].LastName + "," + contactData[i].PhoneNumber + "," + contactData[i].Email);
                }
            }

            out_file.Close();
           simpleSpeak("The contact has been deleted");
            Console.WriteLine("The contact has been deleted");

            

                //loop through array of contacts
                //check to see if contacts f and last name match record you want deleted
                //if they match        do not write to the output file
                //if they dont match   write to the output file
                //close output file
            }

            static string prompt(string msg) {
                Console.WriteLine(msg + " ");
                return Console.ReadLine();

            }

            static void SearchName(string tempPath, string SearchNameVar) {
                StreamReader in_file = new StreamReader(tempPath);
                string text = "";
                char[] split_array = { ',' };
                string[] split_text;
                Contacts[] ContactArray = new Contacts[CountFile(tempPath)];
                while (!in_file.EndOfStream) {
                    text = in_file.ReadLine();
                    split_text = text.Split(split_array);

                    if (SearchNameVar == split_text[0]) {
                        Console.WriteLine(split_text[0] + "," + split_text[1] + "," + split_text[2] + "," + split_text[3]);
                    } else if (SearchNameVar == split_text[1]) {
                        Console.WriteLine(split_text[0] + "," + split_text[1] + "," + split_text[2] + "," + split_text[3]);
                    } else if (SearchNameVar == split_text[2]) {
                        Console.WriteLine(split_text[0] + "," + split_text[1] + "," + split_text[2] + "," + split_text[3]);
                    } else if (SearchNameVar == split_text[3]) {
                        Console.WriteLine(split_text[0] + "," + split_text[1] + "," + split_text[2] + "," + split_text[3]);
                    }

                }
                in_file.Close();
            }

            static int CountFile(string tempPath) {
                int count = 0;
                StreamReader in_file = new StreamReader(tempPath);
                while (!in_file.EndOfStream) {
                    in_file.ReadLine();
                    count++;
                }
                in_file.Close();
                return count;
            }

            static void AddNewRecord(string tempPath) {

                Contacts newRecord;
                Console.WriteLine("Enter the First Name of Contact:");
                newRecord.FirstName = (Console.ReadLine());
                Console.WriteLine("Enter the Last Name of Contact:");
                newRecord.LastName = (Console.ReadLine());
                Console.WriteLine("Enter Phone Number");
                newRecord.PhoneNumber = (Console.ReadLine());
                Console.WriteLine("Enter Email:");
                newRecord.Email = (Console.ReadLine());


                StreamWriter in_file = File.AppendText(tempPath);
                
                in_file.Write(newRecord.FirstName + ",");
                in_file.Write(newRecord.LastName + ",");
                in_file.Write(newRecord.PhoneNumber + ",");
                in_file.Write(newRecord.Email);
                in_file.WriteLine();
                in_file.Close();
                // file open in append mode
                // build string
                // StreamWrite string to file
            }

            static void ModifyEmail(string tempPath, int Count) {
                StreamReader in_file = new StreamReader(tempPath);
                int counter = 0;
                Contacts[] contactData = new Contacts[Count];
                string userFN = prompt("Type the First name of the person whose Email you'd like to modify");
                string userLN = prompt("Type the Last name of the person whose Email you'd like to modify");

                while (!in_file.EndOfStream) {

                    string data = in_file.ReadLine().Trim(' ');
                    string[] contacts = data.Split(',');

                    contactData[counter].FirstName = contacts[0];
                    contactData[counter].LastName = contacts[1];
                    contactData[counter].PhoneNumber = contacts[2];
                    contactData[counter].Email = contacts[3];

                    if (userFN == contactData[counter].FirstName && userLN == contactData[counter].LastName) {
                        contactData[counter].Email = prompt("please enter the new Email");
                        Console.WriteLine("You have entered  " + contactData[counter].Email);
                    }
                    counter++;
                }//END WHILE
                in_file.Close();

                StreamWriter out_file = new StreamWriter(tempPath);
                for (int i = 0; i < contactData.Length; i++) {
                    out_file.WriteLine(contactData[i].FirstName + "," + contactData[i].LastName + "," + contactData[i].PhoneNumber + "," + contactData[i].Email);
                }
                out_file.Close();
            }

            static void ModifyPhoneNumber(string tempPath, int Count) {
                Contacts[] contactData = new Contacts[Count];//array of structs
                StreamReader in_file = new StreamReader(tempPath);
                int counter = 0;
                string userFN = prompt("Type the First name of the person whose number you'd like to modify");
                string userLN = prompt("Type the Last name of the person whose number you'd like to modify");

                while (!in_file.EndOfStream) {

                    string data = in_file.ReadLine().Trim(' ');
                    string[] contacts = data.Split(',');

                    contactData[counter].FirstName = contacts[0];
                    contactData[counter].LastName = contacts[1];
                    contactData[counter].PhoneNumber = contacts[2];
                    contactData[counter].Email = contacts[3];

                    if (userFN == contactData[counter].FirstName && userLN == contactData[counter].LastName) {

                        contactData[counter].PhoneNumber = prompt("please enter the new Phone Number");
                        Console.WriteLine("You have entered  " + contactData[counter].PhoneNumber);
                    }
                    counter++;
                }//END WHILE
                in_file.Close();
                StreamWriter out_file = new StreamWriter(tempPath);
                for (int i = 0; i < contactData.Length; i++) {
                    out_file.WriteLine(contactData[i].LastName + "," + contactData[i].FirstName + "," + contactData[i].PhoneNumber + "," + contactData[i].Email);
                }
                out_file.Close();
            }

            static void SwapArray(int i, int j, Contacts[] ary) {
               Contacts temp;
               if (i != j) {
                temp = ary[i];
                ary[i] = ary[j];
                ary[j] = temp;
                }
            }

            static void Alphabetize(string tempPath, int count) {
                Contacts[] contactData = new Contacts[count];
                StreamReader in_file = new StreamReader(tempPath);
                int index =0;
                while (!in_file.EndOfStream) {
                    string data = in_file.ReadLine();
                    string[] contacts = data.Split(',');

                          contactData[index].FirstName = contacts[0];
                          contactData[index].LastName = contacts[1];
                          contactData[index].PhoneNumber = contacts[2];
                          contactData[index].Email = contacts[3];
                          index++;
                }//End while

                in_file.Close();

                         InsertionSort(contactData);
                
                    StreamWriter Replace = new StreamWriter(tempPath);

            foreach (Contacts temp in contactData)  {
                Replace.WriteLine("{0},{1},{2},{3}", temp.FirstName, temp.LastName, temp.PhoneNumber, temp.Email);

            }
            Replace.Close();
            ListContacts(tempPath);
            }//END FUNCTION

            static int array_Find_Min_Index(Contacts[] array, int StartingPoint) {
           int smallest = StartingPoint;
           for (int i= StartingPoint; i < array.GetLength(0); i += 1) {
               if (stringCompare(array[i].FirstName, array[smallest].FirstName, true)) {
               smallest = i;
               }
           }
           return smallest;
        }//END FUNCTION
         
            static void InsertionSort(Contacts[] array) {
           int minIndex = 0;
           for (int i = 0; i < array.GetLength(0); i++)
           {
               minIndex = array_Find_Min_Index(array, i);
               if (minIndex != i)
               {
                   SwapArray(minIndex, i, array);
               }
           }
       }

            static bool stringCompare(string str1, string str2, bool ignoreCase) {
                       //good function for comparing strings 
                       int maxSize = Math.Min(str1.Length, str2.Length);
       
                       if (ignoreCase) {
                           str1 = str1.ToLower();
                           str2 = str2.ToLower();
                       }
               //   str1.CompareTo(str2) < 0;
                       if (str1 == str2) {
                           return false;
                       }
                       for (int index = 0; index < maxSize; index++) {
                           char l1 = str1[index];
                           char l2 = str2[index];
                           if (l1 > l2) { return false; }
                           if (l1 < l2) { return true; }
                       }//END FOR
                       if (str1.Length > str2.Length) {
                           return false;
                       } else if (str1.Length < str2.Length) {
                           return true;
                       }//END IF
                       return false;
                   }
        
           
            static void setupConsole(ConsoleColor back, ConsoleColor front) {
                       Console.BackgroundColor = back;
                       Console.ForegroundColor = front;
                       Console.Clear();
                   }
          
    }//END CLASS
}//END NAMESPACE
