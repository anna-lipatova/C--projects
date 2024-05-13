class Program {
	static void Main(string[] args) {
		Console.WriteLine("Press ENTER to run without debug prints,");
		Console.WriteLine("Press D1 + ENTER to enable some debug prints,");
		Console.Write("Press D2 + ENTER to enable all debug prints: ");
		string command = Console.ReadLine()!.ToUpper();
		DebugPrints1 = command == "D2" || command == "D1" || command == "D";
		DebugPrints2 = command == "D2";
		Console.WriteLine();

		var groupA = new Group();

		HighlightedWriteLine("Assignment 1: Vsechny osoby," +
			" ktere nepovazuji nikoho za sveho pritele.");

        Console.WriteLine("Main: foreach:");
        var peopleWithoutFriends = from p in groupA where p.Friends.Any() == false select p;
		foreach (var person in peopleWithoutFriends)
		{
            Console.WriteLine($"Main: got {person}");
        }


		Console.WriteLine();
		HighlightedWriteLine("Assignment 2: Vsechny osoby setridene " +
			"vzestupne podle jmena, ktere jsou starsi 15 let, " +
			"a jejichz jmeno zacina na pismeno D nebo vetsi.");

        Console.WriteLine("Main: foreach:");
		var peopleAreOlderThen15AndHavingNameFirstLetterAtLeastD = 
			from p in groupA where p.Age > 15 where p.Name[0] >= 'D' orderby p.Name select p;
		//complexity is O(N) + O(c) + O(c*logc) where c is c < N
		
		foreach (var person in peopleAreOlderThen15AndHavingNameFirstLetterAtLeastD)
		{
            Console.WriteLine($"Main: got {person}");
        }


        Console.WriteLine();
		HighlightedWriteLine("Assignment 3: Vsechny osoby, " +
			"ktere jsou ve skupine nejstarsi, " +
			"a jejichz jmeno zacina na pismeno T nebo vetsi.");

        
        //var theOldestPeopleWithNameFirstLetterAtLeastT = from p in groupA where p.Age == (groupA.Max(p2 => p2.Age)) where p.Name[0] >= 'T' select p;
        //var theOldestPeopleWithNameFirstLetterAtLeastT = from p in groupA let maxAge = groupA.Max(p2 => p2.Age) where p.Age == maxAge where p.Name[0] >= 'T' select p;
        //opakujou se vypisy!!!
        // Main: foreach:
        //Group is being enumerated.
        //Group is being enumerated.
        //All elements of Group have been enumerated.
        //Group is being enumerated.
        //All elements of Group have been enumerated.
        //Group is being enumerated.
        //and so on

        var theOldestAge = groupA.Max(p => p.Age);
		var theOldestPeopleWithNameFirstLetterAtLeastT = from p in groupA where p.Age == theOldestAge where p.Name[0] >= 'T' select p;
        Console.WriteLine("Main: foreach:");
        foreach (var person in theOldestPeopleWithNameFirstLetterAtLeastT)
        {
            Console.WriteLine($"Main: got {person}");
        }


        Console.WriteLine();
		HighlightedWriteLine("Assignment 4: Vsechny osoby, " +
			"ktere jsou starsi nez vsichni jejich pratele.");

        var peopleOlderThenAllTheirFriends = from p in groupA where p.Friends.Any(p2 => p2.Age > p.Age) == false select p;
        Console.WriteLine("Main: foreach:");
        foreach (var person in peopleOlderThenAllTheirFriends)
        {
            Console.WriteLine($"Main: got {person}");
        }


        Console.WriteLine();
		HighlightedWriteLine("Assignment 5: Vsechny osoby, " +
			"ktere nemaji zadne pratele " +
			"(ktere nikoho nepovazuji za sveho pritele," +
			" a zaroven ktere nikdo jiny nepovazuje za sveho pritele).");

        var theMostUnfriendlyPeople = from p in groupA where p.Friends.Any() == false where (from p2 in groupA where p2.Friends.Contains(p) select p2).Any() == false select p;
        Console.WriteLine("Main: foreach:");
        foreach (var person in theMostUnfriendlyPeople)
        {
            Console.WriteLine($"Main: got {person}");
        }


        Console.WriteLine();
		HighlightedWriteLine("Assignment 6: Vsechny osoby, " +
			"ktere jsou necimi nejstarsimi prateli (s opakovanim).");

		//first way
        var theOldestFriends = groupA.Where(
			p => p.Friends.Any()).SelectMany(
			//3. *
			p => { 
				int maxAge = p.Friends.Max(p2 => p2.Age);
				//1. find max age
				return p.Friends.Where(p2 => p2.Age == maxAge);
				//2. return friends with max age
			}	
			);
        //but prints out that bad:
        /*
		 Main: foreach:
*** Group is being enumerated.
 # Person("Hubert").Friends is being enumerated.
 # Person("Anna").Friends is being enumerated.
 # Person("Frantisek").Friends is being enumerated.
 # Person("Frantisek").Friends is being enumerated.
 # Person("Frantisek").Friends is being enumerated.
Main: got Person(Name = "Anna", Age = 22)
 # Person("Blazena").Friends is being enumerated.
 # Person("Blazena").Friends is being enumerated.
 # Person("Blazena").Friends is being enumerated.
Main: got Person(Name = "Ursula", Age = 22)
Main: got Person(Name = "Vendula", Age = 22)
 # Person("Ursula").Friends is being enumerated.
 # Person("Ursula").Friends is being enumerated.
 # Person("Ursula").Friends is being enumerated.
Main: got Person(Name = "Blazena", Age = 18)
Main: got Person(Name = "Daniela", Age = 18)
 # Person("Daniela").Friends is being enumerated.
 # Person("Daniela").Friends is being enumerated.
 # Person("Daniela").Friends is being enumerated.
Main: got Person(Name = "Ursula", Age = 22)
 # Person("Emil").Friends is being enumerated.
 # Person("Emil").Friends is being enumerated.
 # Person("Emil").Friends is being enumerated.
Main: got Person(Name = "Vendula", Age = 22)
 # Person("Vendula").Friends is being enumerated.
 # Person("Vendula").Friends is being enumerated.
 # Person("Vendula").Friends is being enumerated.
Main: got Person(Name = "Emil", Age = 21)
 # Person("Cyril").Friends is being enumerated.
 # Person("Cyril").Friends is being enumerated.
 # Person("Cyril").Friends is being enumerated.
Main: got Person(Name = "Daniela", Age = 18)
 # Person("Gertruda").Friends is being enumerated.
 # Person("Gertruda").Friends is being enumerated.
 # Person("Gertruda").Friends is being enumerated.
Main: got Person(Name = "Frantisek", Age = 15)
*** All elements of Group have been enumerated.
		 */

		//second way
        theOldestFriends = from p in groupA let maxAge = p.Friends.DefaultIfEmpty(new Person { Age = 0, Name = ""}).Max(p2 => p2.Age) from p2 in p.Friends where p2.Age == maxAge select p2;

        Console.WriteLine("Main: foreach:");
        foreach (var person in theOldestFriends)
        {
            Console.WriteLine($"Main: got {person}");
        }

        Console.WriteLine();
		HighlightedWriteLine("Assignment 6B: Vsechny osoby, ktere jsou necimi nejstarsimi prateli (bez opakovani).");

		Console.WriteLine();
		HighlightedWriteLine("Assignment 7: Vsechny osoby, ktere jsou nejstarsimi prateli osoby starsi nez ony samy (s opakovanim).");

		Console.WriteLine();
		HighlightedWriteLine("Assignment 7B: Vsechny osoby, ktere jsou nejstarsimi prateli osoby starsi nez ony samy (bez opakovani).");

		Console.WriteLine();
		HighlightedWriteLine("Assignment 7C: Vsechny osoby, ktere jsou nejstarsimi prateli osoby starsi nez ony samy (bez opakovani a setridene sestupne podle jmena osoby).");
	}

	public static void HighlightedWriteLine(string s) {
		ConsoleColor oldColor = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine(s);
		Console.ForegroundColor = oldColor;
	}

	public static bool DebugPrints1 = false;
	public static bool DebugPrints2 = false;

	// IMPORTANT NOTE: You should NOT modify any code below !!!

	class Person {
		#region Use only this public API of the Person class without modification + use Person() ctor + use ToString() as implemented below
		public required string Name { get; init; }
		public required int Age { get; init; }
		public IEnumerable<Person> Friends { get; private set; }
		#endregion

		/// <summary>
		/// IMPORTANT: DO NOT USE in your LINQ queries!!!
		/// </summary>
		public IList<Person> FriendsListInternal { get; private set; }

		class EnumWrapper<T> : IEnumerable<T> {
			IEnumerable<T> innerEnumerable;
			Person person;
			string propName;

			public EnumWrapper(Person person, string propName, IEnumerable<T> innerEnumerable) {
				this.person = person;
				this.propName = propName;
				this.innerEnumerable = innerEnumerable;
			}

			public IEnumerator<T> GetEnumerator() {
				if (Program.DebugPrints1) Console.WriteLine(" # Person(\"{0}\").{1} is being enumerated.", person.Name, propName);

				foreach (var value in innerEnumerable) {
					yield return value;
				}

				if (Program.DebugPrints2) Console.WriteLine(" # All elements of Person(\"{0}\").{1} have been enumerated.", person.Name, propName);
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
				return GetEnumerator();
			}
		}

		public Person() {
			FriendsListInternal = new List<Person>();
			Friends = new EnumWrapper<Person>(this, "Friends", FriendsListInternal);
		}

		public override string ToString() {
			return string.Format("Person(Name = \"{0}\", Age = {1})", Name, Age);
		}
	}

	class Group : IEnumerable<Person> {
		Person anna, blazena, ursula, daniela, emil, vendula, cyril, frantisek, hubert, gertruda;

		public Group() {
			anna = new Person { Name = "Anna", Age = 22 };
			blazena = new Person { Name = "Blazena", Age = 18 };
			ursula = new Person { Name = "Ursula", Age = 22, FriendsListInternal = { blazena } };
			daniela = new Person { Name = "Daniela", Age = 18, FriendsListInternal = { ursula } };
			emil = new Person { Name = "Emil", Age = 21 };
			vendula = new Person { Name = "Vendula", Age = 22, FriendsListInternal = { blazena, emil } };
			cyril = new Person { Name = "Cyril", Age = 21, FriendsListInternal = { daniela } };
			frantisek = new Person { Name = "Frantisek", Age = 15, FriendsListInternal = { anna, blazena, cyril, daniela, emil } };
			hubert = new Person { Name = "Hubert", Age = 10 };
			gertruda = new Person { Name = "Gertruda", Age = 10, FriendsListInternal = { frantisek } };

			blazena.FriendsListInternal.Add(ursula);
			blazena.FriendsListInternal.Add(vendula);
			ursula.FriendsListInternal.Add(daniela);
			daniela.FriendsListInternal.Add(cyril);
			emil.FriendsListInternal.Add(vendula);
		}

		public IEnumerator<Person> GetEnumerator() {
			if (Program.DebugPrints1) Console.WriteLine("*** Group is being enumerated.");

			yield return hubert;
			yield return anna;
			yield return frantisek;
			yield return blazena;
			yield return ursula;
			yield return daniela;
			yield return emil;
			yield return vendula;
			yield return cyril;
			yield return gertruda;

			if (Program.DebugPrints1) Console.WriteLine("*** All elements of Group have been enumerated.");
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}

