using System;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace banking_system_ass1
{
    class Program
    {
        private string inputUserName;
        private string inputPassword;
        private int accountNo;

        public int AccountNumberGenerate()
        {
            Random rnd = new Random();
            int acc = rnd.Next(100000, 10000000);
            return acc;
        }

        public bool CheckCredentials(string userName, string passWord)
        {
            string[] lines = System.IO.File.ReadAllLines("login.txt");
            foreach (string set in lines)
            {
                // Split each line
                string[] splits = set.Split('|');
                if (splits[0].Equals(userName) && splits[1].Equals(passWord))
                {
                    return true;
                }
            }
            return false;
        }

        public string RestrictInputLength(ref string input, int length)
        {
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    return input;
                }
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Spacebar && input.Length != length)
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                if (key.Key == ConsoleKey.Backspace && input.Length != 0)
                {
                    input = input.Substring(0, input.Length - 1); // Remove last  char from string
                    Console.Write("\b \b"); // Remove last char on display
                }
            } while (true);
        }

        public void LoginMenu()
        {
            do
            {
                inputUserName = "";
                inputPassword = "";
                Console.Clear(); // Clear the console screen
                Console.WriteLine("    ╔════════════════════════════════════════╗");
                Console.WriteLine("    |    WELCOME TO SIMPLE BANKING SYSTEM    |");
                Console.WriteLine("    |════════════════════════════════════════|");
                Console.WriteLine("    |\t          LOGIN TO START             |");
                Console.WriteLine("    |\t                \t\t     |");
                Console.Write("    |         User Name: ");
                int LoginCursorX = Console.CursorTop;
                int LoginCursorY = Console.CursorLeft;
                Console.WriteLine("\t\t     |");
                Console.Write("    |         Password: ");
                int PwdCursorX = Console.CursorTop;
                int PwdCursorY = Console.CursorLeft;
                Console.WriteLine("\t\t     |");
                Console.WriteLine("    ╚════════════════════════════════════════╝");
                Console.SetCursorPosition(LoginCursorY, LoginCursorX);
                // Restrict username length
                RestrictInputLength(ref inputUserName, 10);

                Console.SetCursorPosition(PwdCursorY, PwdCursorX);
                // Replace password with '*'
                char passwordChar = '*';
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Spacebar && inputPassword.Length != 8)
                    {
                        inputPassword += key.KeyChar;
                        Console.Write(passwordChar);
                    }
                    if (key.Key == ConsoleKey.Backspace && inputPassword.Length != 0)
                    {
                        inputPassword = inputPassword.Substring(0, inputPassword.Length - 1); // Remove last  char from string
                        Console.Write("\b \b"); // Remove last char on display
                    }
                } while (true);

                if (CheckCredentials(inputUserName, inputPassword) == true)
                {
                    Console.WriteLine("\n\nValid credentials!... Enter any key to proceed");
                    System.Console.ReadKey();
                    MainMenu();
                    break;
                }

                if (CheckCredentials(inputUserName, inputPassword) == false)
                {
                    string msg = "\n\nInvalid credentials!... Enter  any key to try again";
                    Console.WriteLine(msg);
                    Console.ReadKey(true);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', msg.Length));
                }

            } while (true);
        }

        public void MainMenu()
        {
            Console.Clear();
            string choice;
            Console.WriteLine("    ╔════════════════════════════════════════╗");
            Console.WriteLine("    |    WELCOME TO SIMPLE BANKING SYSTEM    |");
            Console.WriteLine("    |════════════════════════════════════════|");
            Console.WriteLine("    |     1. Create a new account            |");
            Console.WriteLine("    |     2. Search for an account           |");
            Console.WriteLine("    |     3. Deposit                         |");
            Console.WriteLine("    |     4. Withdraw                        |");
            Console.WriteLine("    |     5. A/C statement                   |");
            Console.WriteLine("    |     6. Delete account                  |");
            Console.WriteLine("    |     7. Exit                            |");
            Console.WriteLine("    ╚════════════════════════════════════════╝");
            Console.Write("    |    Enter your choice (1-7): ");
            int CursorX = Console.CursorLeft;
            int CursorY = Console.CursorTop;
            Console.WriteLine("\t     |\n    ╚════════════════════════════════════════╝");
            Console.SetCursorPosition(CursorX, CursorY);
            ConsoleKeyInfo input = Console.ReadKey(); ;
                
                // Field checks
            switch (choice = input.KeyChar.ToString())
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    SearchAccount();
                    break;
                case "3":
                    Deposit();
                    break;
                case "4":
                    Withdraw();
                    break;
                case "5":
                    AccountStatement();
                    break;
                case "6":
                    DeleteAccount();
                    break;
                case "7":
                    break;
                default:
                    MainMenu();
                    break;
            }
        }

        public void DeleteAccount()
        {
            string accSearch = "";
            Console.Clear();
            Console.WriteLine("    ╔═════════════════════════════════════════╗");
            Console.WriteLine("    |             DELETE AN ACCOUNT           |");
            Console.WriteLine("    |═════════════════════════════════════════|");
            Console.WriteLine("    |            ENTER THE DETAILS            |");
            Console.WriteLine("    |                                         |");
            Console.Write("    |     Account Number:  ");
            int X = Console.CursorLeft;
            int Y = Console.CursorTop;
            Console.WriteLine("\t\t      |");
            Console.WriteLine("    ╚═════════════════════════════════════════╝");
            Console.SetCursorPosition(X, Y);
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Spacebar && accSearch.Length != 10)
                {
                    if (char.IsDigit(key.KeyChar))
                    {
                        accSearch += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                if (key.Key == ConsoleKey.Backspace && accSearch.Length != 0)
                {
                    accSearch = accSearch.Substring(0, accSearch.Length - 1); // Remove last  char from string
                    Console.Write("\b \b"); // Remove last char on display
                }
            } while (true);
            if (File.Exists(accSearch + ".txt"))
            {
                Console.Write("\n\n\n    Account found! Details displayed below...");
                AccountDetails(Convert.ToInt32(accSearch));
                Console.WriteLine("\n\n\n    Delete (y/n)?");
                Console.SetCursorPosition(18, Console.CursorTop - 1);
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.Y)
                {
                    File.Delete(accSearch + ".txt");
                    Console.WriteLine("\n    Account Deleted!...");
                    Console.ReadKey();
                    MainMenu();
                }
                if (input.Key == ConsoleKey.N)
                {
                    MainMenu();

                }
            }
            else
            {
                Console.Write("\n\n\n    Account not found!");
                Console.WriteLine("\n    Retry (y/n)?");
                Console.SetCursorPosition(17, Console.CursorTop - 1);
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.Y)
                {
                    DeleteAccount();
                }
                if (input.Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }
        }

        public void AccountStatement()
        {
            string accSearch = "";
            int aX, aY;
            Console.Clear();
            Console.WriteLine("    ╔═════════════════════════════════════════╗");
            Console.WriteLine("    |                 STATEMENT               |");
            Console.WriteLine("    |═════════════════════════════════════════|");
            Console.WriteLine("    |            ENTER THE DETAILS            |");
            Console.WriteLine("    |                                         |");
            Console.Write("    |     Account Number:  ");
            int X = Console.CursorLeft;
            int Y = Console.CursorTop;
            Console.WriteLine("\t\t      |");
            Console.WriteLine("    ╚═════════════════════════════════════════╝");
            Console.SetCursorPosition(X, Y);
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Spacebar && accSearch.Length != 10)
                {
                    if (char.IsDigit(key.KeyChar))
                    {
                        accSearch += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                if (key.Key == ConsoleKey.Backspace && accSearch.Length != 0)
                {
                    accSearch = accSearch.Substring(0, accSearch.Length - 1); // Remove last  char from string
                    Console.Write("\b \b"); // Remove last char on display
                }
            } while (true);
            if (File.Exists(accSearch + ".txt"))
            {
                Console.Write("\n\n\n    Account found! The statement is displayed below...");
                Console.WriteLine("\n    ╔═════════════════════════════════════════╗");
                Console.WriteLine("    |          SIMPLE BANKING SYSTEM          |");
                Console.WriteLine("    |═════════════════════════════════════════|");
                Console.WriteLine("    |    Account Statement                    |");
                Console.WriteLine("    |                                         |");
                Console.WriteLine("    |     Date     |  Action  | +/- |   Net   |");
                Console.WriteLine("    |-----------------------------------------|");
                
                // Read from specified line
                string[] lines = File.ReadAllLines(accSearch + ".txt");
                for (int i = 1; i < 6; i++)
                {
                    if (lines[lines.Count() - i].Length == 0)
                    {
                        Console.WriteLine("    |                                         |");
                    }
                    else
                    {
                        Console.Write("    |   " + lines[lines.Count() - i] + "    ");
                        aX = Console.CursorLeft;
                        aY = Console.CursorTop;
                        Console.SetCursorPosition(aX, aY);
                        Console.Write("|\n");

                    }
                }
                Console.WriteLine("    ╚═════════════════════════════════════════╝");

                // Send Email
                string[] temp = System.IO.File.ReadAllLines(accSearch + ".txt");
                List<string> values = new List<string>();
                foreach (string set in temp)
                {
                    if (set.Length != 0)
                    {
                        string[] splits = set.Split('|');
                        values.Add(splits[1]);
                    }
                }
                MailAddress to = new MailAddress(values[4]);
                MailAddress from = new MailAddress("noreply.simplebanking@gmail.com");
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "Here's your account statement";
                mail.Body = ("        Date     |   Action   |   +/-  |   Net   "
                            + Environment.NewLine + "---------------------------------------------------"
                            + Environment.NewLine + lines[lines.Count() - 1]
                            + Environment.NewLine + lines[lines.Count() - 2]
                            + Environment.NewLine + lines[lines.Count() - 3]
                            + Environment.NewLine + lines[lines.Count() - 4]
                            + Environment.NewLine + lines[lines.Count() - 5]);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential(
                    "noreply.simplebanking", "2asb1291");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Console.ReadKey();
                MainMenu();
            }
            else
            {
                Console.Write("\n\n\n    Account not found!");
                Console.WriteLine("\n    Retry (y/n)?");
                Console.SetCursorPosition(17, Console.CursorTop - 1);
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.Y)
                {
                    AccountStatement();
                }
                if (input.Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }
        }

        public void Withdraw()
        {
            string accSearch = "";
            string amount = "";
            string currentAmount = "";
            Console.Clear();
            Console.WriteLine("    ╔════════════════════════════════════════╗");
            Console.WriteLine("    |                WITHDRAW                |");
            Console.WriteLine("    |════════════════════════════════════════|");
            Console.WriteLine("    |            ENTER THE DETAILS           |");
            Console.WriteLine("    |                                        |");
            Console.Write("    |     Account Number:  ");
            int X = Console.CursorLeft;
            int Y = Console.CursorTop;
            Console.WriteLine("\t\t     |");
            Console.Write("    |     Amount:  $");
            int aX = Console.CursorLeft;
            int aY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.WriteLine("    ╚════════════════════════════════════════╝");
            Console.SetCursorPosition(X, Y);
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Spacebar && accSearch.Length != 10)
                {
                    if (char.IsDigit(key.KeyChar))
                    {
                        accSearch += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                if (key.Key == ConsoleKey.Backspace && accSearch.Length != 0)
                {
                    accSearch = accSearch.Substring(0, accSearch.Length - 1); // Remove last  char from string
                    Console.Write("\b \b"); // Remove last char on display
                }
            } while (true);
            if (File.Exists(accSearch + ".txt"))
            {
                Console.Write("\n\n\n    Account found! Enter the amount...");
                Console.SetCursorPosition(aX, aY);
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Spacebar && amount.Length != 8)
                    {
                        if (char.IsDigit(key.KeyChar))
                        {
                            amount += key.KeyChar;
                            Console.Write(key.KeyChar);
                        }
                    }
                    if (key.Key == ConsoleKey.Backspace && amount.Length != 0)
                    {
                        amount = amount.Substring(0, amount.Length - 1); // Remove last  char from string
                        Console.Write("\b \b"); // Remove last char on display
                    }
                } while (true);

                string[] lines = System.IO.File.ReadAllLines(accSearch + ".txt");
                foreach (string set in lines) // Get current balance
                {
                    string[] splits = set.Split("| $");
                    if (splits[0].Contains("Balance"))
                    {
                        currentAmount = splits[1];
                    }
                }
                int newAmount = Convert.ToInt32(currentAmount) - Convert.ToInt32(amount);
                if (newAmount >= 0)
                {
                    string file3 = accSearch + ".txt";
                    string date = DateTime.Now.ToString("dd-MM-yyyy");


                    // Add transaction history
                    File.WriteAllText(accSearch + ".txt", File.ReadAllText(accSearch + ".txt").Replace("$"+currentAmount, "$"+newAmount.ToString()));
                    File.AppendAllText(file3, date + " | " + "Withdraw" + " | -" + amount + " | " + newAmount + Environment.NewLine);
                    Console.WriteLine("\n\n\n    Withdraw successfull!");

                    Console.ReadKey();
                    MainMenu();
                }
                else
                {
                    Console.WriteLine("\n\n\n    Not enough balance...please try again.");
                    Console.ReadKey();
                    Withdraw();
                }
            }
            else
            {
                Console.Write("\n\n\n    Account not found!");
                Console.WriteLine("\n    Retry (y/n)?");
                Console.SetCursorPosition(17, Console.CursorTop - 1);
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.Y)
                {
                    Withdraw();
                }
                if (input.Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }
        }

        public void Deposit()
        {
            string accSearch = "";
            string amount = "";
            string currentAmount = "";
            Console.Clear();
            Console.WriteLine("    ╔════════════════════════════════════════╗");
            Console.WriteLine("    |                DEPOSIT                 |");
            Console.WriteLine("    |════════════════════════════════════════|");
            Console.WriteLine("    |            ENTER THE DETAILS           |");
            Console.WriteLine("    |                                        |");
            Console.Write("    |     Account Number:  ");
            int X = Console.CursorLeft;
            int Y = Console.CursorTop;
            Console.WriteLine("\t\t     |");
            Console.Write("    |     Amount:  $");
            int aX = Console.CursorLeft;
            int aY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.WriteLine("    ╚════════════════════════════════════════╝");
            Console.SetCursorPosition(X, Y);
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Spacebar && accSearch.Length != 10)
                {
                    if (char.IsDigit(key.KeyChar))
                    {
                        accSearch += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                if (key.Key == ConsoleKey.Backspace && accSearch.Length != 0)
                {
                    accSearch = accSearch.Substring(0, accSearch.Length - 1); // Remove last  char from string
                    Console.Write("\b \b"); // Remove last char on display
                }
            } while (true);
            if (File.Exists(accSearch + ".txt"))
            {
                Console.Write("\n\n\n    Account found! Enter the amount...");
                Console.SetCursorPosition(aX, aY);
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Spacebar && amount.Length != 8)
                    {
                        if (char.IsDigit(key.KeyChar))
                        {
                            amount += key.KeyChar;
                            Console.Write(key.KeyChar);
                        }
                    }
                    if (key.Key == ConsoleKey.Backspace && amount.Length != 0)
                    {
                        amount = amount.Substring(0, amount.Length - 1); // Remove last  char from string
                        Console.Write("\b \b"); // Remove last char on display
                    }
                } while (true);

                string[] lines = System.IO.File.ReadAllLines(accSearch + ".txt");
                foreach (string set in lines) // Get current balance
                {
                    string[] splits = set.Split("| $");
                    if (splits[0].Contains("Balance"))
                    {
                        currentAmount = splits[1];
                    }
                }

                // Add transaction history
                int newAmount = Convert.ToInt32(amount) + Convert.ToInt32(currentAmount);
                string file2 = accSearch + ".txt";
                string date = DateTime.Now.ToString("dd-MM-yyyy");

                File.WriteAllText(accSearch + ".txt", File.ReadAllText(accSearch + ".txt").Replace("$" + currentAmount, "$" + newAmount.ToString()));
                File.AppendAllText(file2, date + " | " + "Deposit" + " | +" + amount + " | " + newAmount + Environment.NewLine);
                Console.WriteLine("\n\n\n    Deposit successfull!");

                Console.ReadKey();
                MainMenu();
            }
            else
            {
                Console.Write("\n\n\n    Account not found!");
                Console.WriteLine("\n    Retry (y/n)?");
                Console.SetCursorPosition(17, Console.CursorTop - 1);
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.Y)
                {
                    Deposit();
                }
                if (input.Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }
        }

        public void CreateAccount()
        {
            string firstName = "",
                   lastName = "",
                   address = "",
                   email = "",
                   phone = "";
            string[] info = new string[12];

            Console.Clear();
            Console.WriteLine("    ╔════════════════════════════════════════╗");
            Console.WriteLine("    |         CREATE A NEW ACCOUNT           |");
            Console.WriteLine("    |════════════════════════════════════════|");
            Console.WriteLine("    |            ENTER THE DETAILS           |");
            Console.WriteLine("    |                                        |");
            Console.Write("    |     First Name:  ");
            int fX = Console.CursorLeft;
            int fY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.Write("    |     Last Name:  ");
            int lX = Console.CursorLeft;
            int lY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.Write("    |     Address:  ");
            int aX = Console.CursorLeft;
            int aY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.Write("    |     Phone:  ");
            int pX = Console.CursorLeft;
            int pY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.Write("    |     Email:  ");
            int eX = Console.CursorLeft;
            int eY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.WriteLine("    ╚════════════════════════════════════════╝");
            Console.SetCursorPosition(fX, fY);
            RestrictInputLength(ref firstName, 10);
            info[0] = "First Name | " + firstName;
            Console.SetCursorPosition(lX, lY);
            RestrictInputLength(ref lastName, 10);
            info[1] = "Last Name | " + lastName;
            Console.SetCursorPosition(aX, aY);
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (key.Key != ConsoleKey.Backspace && address.Length != 15)
                {
                    address += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                if (key.Key == ConsoleKey.Backspace && address.Length != 0)
                {
                    address = address.Substring(0, address.Length - 1); // Remove last  char from string
                    Console.Write("\b \b"); // Remove last char on display
                }
                if (key.Key == ConsoleKey.Spacebar && address.Length < 15)
                {
                    Console.Write(""); // Remove last char on display
                }
            } while (true);
            info[2] = "Address | " + address;
            Console.SetCursorPosition(pX, pY);
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Spacebar && phone.Length != 15)
                {
                    if (char.IsDigit(key.KeyChar))
                    {
                        phone += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                if (key.Key == ConsoleKey.Backspace && phone.Length != 0)
                {
                    phone = phone.Substring(0, phone.Length - 1); // Remove last  char from string
                    Console.Write("\b \b"); // Remove last char on display
                }
            } while (true);
            info[3] = "Phone | " + phone;
            Console.SetCursorPosition(eX, eY);
            RestrictInputLength(ref email, 28);
            info[4] = "Email | " + email;
            Console.WriteLine("\n\n\nIs the information correct (y/n)?");
            bool containsAt = false;
            ConsoleKeyInfo input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Y)
            {
                if (email.Length > 5)
                {
                    foreach (char c in email)
                    {
                        if (c == '@')
                        {
                            // Send Email
                            try
                            {
                                accountNo = AccountNumberGenerate();
                                MailAddress to = new MailAddress(email);
                                MailAddress from = new MailAddress("noreply.simplebanking@gmail.com");
                                MailMessage mail = new MailMessage(from, to);
                                mail.Subject = "Welcome to Simplebanking " + firstName;
                                mail.Body = "Heres your account details: " + Environment.NewLine + "First Name: " + firstName
                                                                           + Environment.NewLine + "Last Name: " + lastName
                                                                           + Environment.NewLine + "Address: " + address
                                                                           + Environment.NewLine + "Phone: " + phone
                                                                           + Environment.NewLine + "Email: " + email
                                                                           +Environment.NewLine + "AccountID: " + accountNo;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.UseDefaultCredentials = false;
                                smtp.Port = 587;
                                smtp.Credentials = new NetworkCredential(
                                    "noreply.simplebanking", "2asb1291");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);

                                containsAt = true;
                                Console.WriteLine("\n\nAccount Created! details will be provided via email.");
                                if (!File.Exists(accountNo + ".txt"))
                                {
                                    Console.WriteLine("\n\nAccount number is: {0}", accountNo);
                                    info[5] = "AccoutNo | " + accountNo;
                                    info[6] = "Balance | $" + 1000;
                                    System.IO.File.WriteAllLines(accountNo + ".txt", info);

                                    Console.ReadKey(true);
                                    MainMenu();
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\n\nPlease enter a valid email...press any key to try again.");
                                Console.ReadKey(true);
                                CreateAccount();
                            }
                        }
                    }
                    if (containsAt == false)
                    {
                        Console.WriteLine("\nPlease enter a valid email...press any key to try again.");
                        Console.ReadKey(true);
                        CreateAccount();
                    }
                }
                else
                {
                    Console.WriteLine("\nPlease enter a valid email...press any key to try again.");
                    Console.ReadKey(true);
                    CreateAccount();
                }
            }
            if(input.Key == ConsoleKey.N)
            {
                CreateAccount();
            }
        }

        public void SearchAccount()
        {
            string accSearch = "";
            Console.Clear();
            Console.WriteLine("    ╔════════════════════════════════════════╗");
            Console.WriteLine("    |            SEARCH AN ACCOUNT           |");
            Console.WriteLine("    |════════════════════════════════════════|");
            Console.WriteLine("    |            ENTER THE DETAILS           |");
            Console.WriteLine("    |                                        |");
            Console.Write("    |     Account Number:  ");
            int X = Console.CursorLeft;
            int Y = Console.CursorTop;
            Console.WriteLine("\t\t     |");
            Console.WriteLine("    ╚════════════════════════════════════════╝");
            Console.SetCursorPosition(X, Y);
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Spacebar && accSearch.Length != 10)
                {
                    if (char.IsDigit(key.KeyChar))
                    {
                        accSearch += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                if (key.Key == ConsoleKey.Backspace && accSearch.Length != 0)
                {
                    accSearch = accSearch.Substring(0, accSearch.Length - 1); // Remove last  char from string
                    Console.Write("\b \b"); // Remove last char on display
                }
            } while (true);
                if (File.Exists(accSearch + ".txt"))
                {
                    Console.Write("\n\n\n    Account found!"); // Remove last char on display
                    AccountDetails(Convert.ToInt32(accSearch));
                    Console.WriteLine("\n\n\n    Check another account (y/n)?");
                    Console.SetCursorPosition(33, Console.CursorTop - 1);
                    ConsoleKeyInfo input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Y)
                    {
                        SearchAccount();
                    }
                    if (input.Key == ConsoleKey.N)
                    {
                        MainMenu();
                    }
                }
                else
                {
                Console.Write("\n\n\n    Account not found!"); // Remove last char on display
                Console.WriteLine("\n    Retry (y/n)?");
                Console.SetCursorPosition(17, Console.CursorTop - 1);
                ConsoleKeyInfo input = Console.ReadKey();
                if(input.Key == ConsoleKey.Y)
                {
                    SearchAccount();
                }
                if(input.Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }
        }

        public void AccountDetails(int account)
        {
            string[] lines = System.IO.File.ReadAllLines(account + ".txt");
            List<string> values = new List<string>();
            foreach (string set in lines)
            {
                if (set.Length != 0)
                {
                    string[] splits = set.Split('|');
                    values.Add(splits[1]);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("    ╔════════════════════════════════════════╗");
            Console.WriteLine("    |             ACCOUNT DETAILS            |");
            Console.WriteLine("    |════════════════════════════════════════|");
            Console.WriteLine("    |                                        |");
            Console.Write("    |     First Name:  ");
            int fX = Console.CursorLeft;
            int fY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.Write("    |     Last Name:  ");
            int lX = Console.CursorLeft;
            int lY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.Write("    |     Address:  ");
            int aX = Console.CursorLeft;
            int aY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.Write("    |     Phone:  ");
            int pX = Console.CursorLeft;
            int pY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.Write("    |     Email:  ");
            int eX = Console.CursorLeft;
            int eY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.Write("    |     Account No:  ");
            int nX = Console.CursorLeft;
            int nY = Console.CursorTop;
            Console.WriteLine("\t\t\t     |");
            Console.Write("    |     AccountBalance:  ");
            int bX = Console.CursorLeft;
            int bY = Console.CursorTop;
            Console.WriteLine("\t\t     |");
            Console.WriteLine("    ╚════════════════════════════════════════╝");
            Console.SetCursorPosition(fX, fY);
            Console.Write(values[0]);
            Console.SetCursorPosition(lX, lY);
            Console.Write(values[1]);
            Console.SetCursorPosition(aX, aY);
            Console.Write(values[2]);
            Console.SetCursorPosition(pX, pY);
            Console.Write(values[3]);
            Console.SetCursorPosition(eX, eY);
            Console.Write(values[4]);
            Console.SetCursorPosition(nX, nY);
            Console.Write(values[5]);
            Console.SetCursorPosition(bX, bY);
            Console.Write(values[6]);
        }

        static void Main(string[] args)
        {
            Program a = new Program();
             a.LoginMenu(); // Start
            
        }
    }
}

