namespace dtp6_contacts
{
    class MainClass
    {
        //static Person[] contactList = new Person[100];
        static List<Person> contactList = new List<Person>();
        class Person
        {
            public string persname, surname, birthdate;
            public string[] phone = new string[100];
            public string[] address = new string[100];
            
            public void PrintInfo()
            {
                Console.Write($"Name: {this.persname} {this.surname}, Phone number(s): ");
                for(int i = 0; i < this.phone.Length; i++)
                {
                    if (this.phone[i] != null)
                        Console.Write(this.phone[i] + ", ");
                }
                Console.Write("Address(es): ");
                for(int i = 0; i < this.address.Length; i++)
                {
                    if (this.address[i] != null)
                        Console.Write(this.address[i] + ", ");
                }
                Console.Write($"Birthdate: {this.birthdate} \n");
            }

        }
        static string[] Input(string s)
        {
            Console.Write(s);
            return Console.ReadLine().Split(' ');
        }
        static void Help()
        {
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  load        - load contact list data from the file address.lis");
            Console.WriteLine("  load /file/ - load contact list data from the file");
            Console.WriteLine("  new        - create new person");
            Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
            Console.WriteLine("  quit        - quit the program");
            Console.WriteLine("  save         - save contact list data to the file previously loaded");
            Console.WriteLine("  save /file/ - save contact list data to the file");
            Console.WriteLine();
        }
        private static void ReadMethod(string lastFileName)
        {

            using (StreamReader infile = new StreamReader(lastFileName))
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    string[] attrs = line.Split('|'); //name | surname | p1, p2.. pn | adr1, adr2... adrn |
                    Person p = new Person();
                    p.persname = attrs[0];
                    p.surname = attrs[1];
                    string[] phones = attrs[2].Split(';');
                    for (int i = 0; i < phones.Length; i++)
                    {
                        p.phone[i] = phones[i];
                    }
                    string[] addresses = attrs[3].Split(';');
                    for (int i = 0; i < addresses.Length; i++)
                    {
                        p.address[i] = addresses[i];
                    }
                    contactList.Add(p);
                }
            }
        }
        private static void NewPerson(List<string> tempName)
        {
            Person p = new Person();
            if (tempName.Count == 0)
            {
                Console.Write("personal name: ");
                p.persname = Console.ReadLine();
                Console.Write("surname: ");
                p.surname = Console.ReadLine();
            }
            else
            {
                p.persname = tempName[0];
                p.surname = tempName[1];
            }
            Console.Write("phone: ");
            p.phone = Console.ReadLine().Split(';');
            Console.Write("address: ");
            p.address = Console.ReadLine().Split(';');
            Console.Write("birthdate: ");
            p.birthdate = Console.ReadLine();
            contactList.Add(p);
        }
        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
            string[] commandLine;
            Console.WriteLine("Hello and welcome to the contact list");
            Help();
            do
            {
                commandLine = Input("> ");
                if (commandLine[0] == "quit")
                {
                    // NYI!
                    Console.WriteLine("Not yet implemented: safe quit");
                }
                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2)
                    {
                        lastFileName = "address.lis";
                        ReadMethod(lastFileName);
                    }
                    else
                    {
                        lastFileName = commandLine[1];
                        ReadMethod(lastFileName);
                    }
                }
                else if (commandLine[0] ==  "list")
                {
                    foreach(Person p in contactList)
                    {
                        p.PrintInfo();
                    }
                }
                else if (commandLine[0] == "save")
                {
                    if (commandLine.Length < 2)
                    {
                        using (StreamWriter outfile = new StreamWriter(lastFileName))
                        {
                            foreach (Person p in contactList)
                            {
                                if (p != null)
                                    outfile.WriteLine($"{p.persname};{p.surname};{p.phone};{p.address};{p.birthdate}");
                            }
                        }
                    }
                    else
                    {
                        // NYI!
                        Console.WriteLine("Not yet implemented: save /file/");
                    }
                }
                else if (commandLine[0] == "new")
                {
                    List<string> tempName = new List<string>();
                    if (commandLine.Length > 2)
                    {
                        tempName.Add(commandLine[1]);
                        tempName.Add(commandLine[2]);
                    }
                    NewPerson(tempName);

                }
                else if (commandLine[0] == "help")
                {
                    Help();
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
                }
            } while (commandLine[0] != "quit");
        }
    }
}
