using gIdeas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gIdeas.DataGenerator
{
    public class DataGen
    {
        public IEnumerable<gDepartment> Departments
        {
            get => new List<gDepartment>
            {
                new gDepartment{ Id= 1, Name = "Computer Science"},
                new gDepartment{ Id= 2, Name = "Games and Digital Media"},
                new gDepartment{ Id= 3, Name = "Maths"},
                new gDepartment{ Id= 4, Name = "Criminology"},
                new gDepartment{ Id= 5, Name = "Law"},
                new gDepartment{ Id= 6, Name = "Drama"},
                new gDepartment{ Id= 7, Name = "English"},
                new gDepartment{ Id= 8, Name = "History"},
                new gDepartment{ Id= 9, Name = "International Relations"},
                new gDepartment{ Id= 10, Name = "Sociology"},
                new gDepartment{ Id= 11, Name = "Architecture"},
                new gDepartment{ Id= 12, Name = "Creative Arts"},
                new gDepartment{ Id= 13, Name = "Media Arts"},
                new gDepartment{ Id= 14, Name = "Construction"},
                new gDepartment{ Id= 15, Name = "Computer Science"},
                new gDepartment{ Id= 16, Name = "Accounting & Finance"},
                new gDepartment{ Id= 17, Name = "Human Resources & Organisational Behaviour"},

            };
        }
        public IEnumerable<gRole> Roles
        {
            get => new List<gRole>
            {
                new gRole{ Id=1, Name="Admin", AccessClaim=gAppConst.AccessClaims.Admin},
                new gRole{ Id=2, Name="Staff", AccessClaim=gAppConst.AccessClaims.Staff},
                new gRole{ Id=3, Name="QA Coordinator", AccessClaim=gAppConst.AccessClaims.QACoordinator},
                new gRole{ Id=4, Name="QA Manager", AccessClaim=gAppConst.AccessClaims.QAManager},
            };

        }

        public List<gUser> GetUserList(bool addDefaulUser = false)
        {
            List<gUser> UserList = new List<gUser>();

            foreach (var dep in Departments)
            {
                foreach (var role in Roles)
                {
                    var name = GetRandomFirstName();
                    var surname = GetRandomSurname();
                    UserList.Add(new gUser
                    {
                        FirstName = name,
                        Surname = surname,
                        Email = $"{name}.{surname}{new Random().Next(0, 100)}@p8b.com",
                        PasswordHash = "As!2",
                        Role = role,
                        Department = dep,
                    });
                }
                for (int i = 0; i < 5; i++)
                {
                    foreach (var role in Roles)
                    {
                        var name = GetRandomFirstName();
                        var surname = GetRandomSurname();
                        UserList.Add(new gUser
                        {
                            FirstName = name,
                            Surname = surname,
                            Email = $"{name}.{surname}{new Random().Next(0, 100)}@p8b.com",
                            PasswordHash = "As!2",
                            Role = Roles.Where(r => r.Name.Contains("Staff")).FirstOrDefault(),
                            Department = dep,
                        });
                    }
                }
            }

            return UserList;
        }

        private string GetRandomFirstName()
        {
            var exampleFirstName = new string[] { "Oprah", "Caldwell", "Miranda", "Yoko", "Keegan", "Guinevere", "Larissa", "Mason", "Merrill", "Nero", "Stephen", "Claudia", "Doris", "Rahim", "Taylor", "Zelenia", "Natalie", "Sloane", "Brock", "Gray", "Ebony", "Adena", "Hoyt", "Brent", "Kevin", "Mira", "Ria", "Boris", "Courtney", "Ciara", "Iona", "Karly", "Connor", "Nero", "Kerry", "Lars", "Irma", "Uma", "Mara", "Ima", "Jin", "Zane", "Porter", "Delilah", "Fiona", "Griffith", "Leonard", "Emery", "Lareina", "Zeus", "Carson", "Honorato", "Tanek", "Michelle", "Kessie", "Sylvia", "Vernon", "Elliott", "Ross", "Suki", "Mary", "Portia", "Forrest", "Nora", "Garrison", "Bree", "Chiquita", "Caldwell", "Felix", "Chanda", "Boris", "Lani", "Keelie", "Craig", "Piper", "Arsenio", "Zephr", "Darius", "Lesley", "Brynn", "Amena", "Damon", "Tatiana", "Elizabeth", "Omar", "Boris", "Jackson", "Arden", "Demetria", "Martin", "Emi", "Lewis", "Micah", "Marsden", "Melanie", "Wyatt", "Ruth", "Indira", "Knox", "Aladdin", "Meghan", "Idola", "Hannah", "Nash", "Lynn", "Cara", "MacKenzie", "Yolanda", "Josiah", "Edward", "Courtney", "Ramona", "Porter", "Nicole", "Nola", "Chelsea", "Sopoline", "Quinn", "Clinton", "Alvin", "Griffith", "Iona", "Wallace", "Ishmael", "Samson", "Lance", "Lucian", "Prescott", "Latifah", "Wesley", "Kirsten", "Adrienne", "Channing", "Lana", "Winifred", "Odessa", "Devin", "Nelle", "Patrick", "Norman", "Ignacia", "Hayfa", "Oleg", "Fallon", "Clarke", "Rina", "Chase", "Erasmus", "Tate", "Madison", "Clio", "Bell", "Scott", "Melvin", "Howard", "Uriel", "Rhoda", "Gannon", "Emery", "Skyler", "Gisela", "Meredith", "Noel", "Giselle", "Rae", "Tara", "Skyler", "Kelly", "August", "Lane", "Roman", "Odysseus", "Quentin", "Eaton", "Jada", "Alexa", "Jason", "Shafira", "Boris", "Hanna", "Nolan", "Melyssa", "Preston", "Hillary", "Nola", "Flavia", "Dacey", "Eagan", "Adara", "Kameko", "Burke", "Nola", "Tanek", "Judah", "Marny", "Moses", "Edward", "Kelsey", "Keefe", "Adele", "Belle", "Wynne", "Ocean", "Serena", "Amos", "Sebastian", "Plato", "Lucas", "Chava", "Alea", "Tiger", "Kevin", "Jonas", "Shelley", "Deirdre", "Amber", "Emerson", "Alfonso", "Alisa", "Paloma", "Jayme", "Dylan", "Salvador", "Mariko", "Thor", "Lana", "MacKensie", "Alexis", "Madison", "Timon", "Venus", "Chantale", "Leo", "Kirk", "Wyatt", "Fuller", "Lysandra", "Sebastian", "Chloe", "Josiah", "Astra", "Maris", "Acton", "Aquila", "Astra", "Jolene", "Idona", "Tanner", "Candace", "Colette", "Brynne", "Ira", "Stone", "August", "Ahmed", "Stephen", "Wynter", "Alika", "Elliott", "Vaughan", "Talon", "Tanner", "Anne", "Brenda", "Alice", "Shana", "Neville", "Marsden", "Len", "Germane", "Kameko", "Jackson", "Leonard", "Yvonne", "Whitney", "Fitzgerald", "Cade", "Dean", "Jordan", "Kaye", "Cheryl", "Tashya", "Jamalia", "Brenda", "Ashton", "Hayley", "Malachi", "India", "Kellie", "Oleg", "Juliet", "Neil", "Barclay", "Serina", "Ava", "Kiayada", "Hayes", "Ira", "Zephr", "Nichole", "Stewart", "Talon", "Kitra", "Hadley", "Serena", "Coby", "Leroy", "Basia", "Drew", "Lionel", "Giacomo", "Mechelle", "Ryder", "Lila", "Sage", "Price", "Cassandra", "Richard", "Berk", "Haviva", "Harriet", "Dean", "Yetta", "Robert", "Peter", "Shad", "Zenaida", "Hiram", "Colette", "Galvin", "Diana", "Ross", "Berk", "Aquila", "Guinevere", "Zahara", "Jolie", "Baxter", "Ibrahim", "Aurelia", "Norman", "Lawrence", "Silas", "Brielle", "Ashely", "Guy", "Hyatt", "Melyssa", "Timon", "Claudia", "Ifeoma", "Tanner", "Alan", "Jana", "Renee", "Arsenio", "Helen", "Tucker", "James", "John", "Fredericka", "Shelly", "Fritz", "Jamal", "Amelia", "Lana", "Lane", "Ahmed", "MacKensie", "Cain", "Imani", "Imogene", "Miranda", "Imogene", "Deirdre", "Myles", "Libby", "Vladimir", "Ali", "Joshua", "Drake", "Yardley", "Whitney", "Risa", "Iris", "Raya", "Cassady", "Madison", "Keiko", "Kevin", "Beck", "Hasad", "Summer", "Graham", "Luke", "Renee", "Indigo", "Elliott", "Sara", "Dalton", "Megan", "Wesley", "Aaron", "Maxine", "Pearl", "Wyoming", "Tarik", "Eric", "Raya", "Raja", "Fredericka", "Irene", "Chelsea", "Ainsley", "Belle", "Donovan", "Hoyt", "Yuri", "John", "Jena", "Gabriel", "John", "Nell", "Kirby", "Ariana", "Lunea", "Gail", "Cassidy", "Kane", "John", "Kristen", "Darius", "Silas", "Rylee", "Tad", "Boris", "Uriah", "Tatyana", "Wallace", "Aladdin", "Autumn", "Lamar", "Colby", "Fleur", "Adena", "Dominic", "Raphael", "Lynn", "Christen", "Kyle", "Shelley", "Cairo", "Marny", "Rhona", "Mollie", "Richard", "Macaulay", "Keefe", "Tanner", "Jescie", "Adrienne", "Timon", "Risa", "Shelby", "Autumn", "Upton", "Aidan", "Demetria", "Penelope", "Aquila", "Nevada", "Amity", "Hayley", "Zena", "Caleb", "Otto", "Nyssa", "Emily", "Paki", "Adena", "Aileen", "Duncan", "Madonna", "Carolyn", "Echo", "Michael", "Raymond", "Hyacinth", "Constance", "Sara", "Hannah", "Gareth", "Shafira", "Amaya", "Helen", "Alisa", "Glenna", "Aiko", "Echo", "Kuame" };
            return exampleFirstName[new Random().Next(0, exampleFirstName.Length)];
        }
        private string GetRandomSurname()
        {
            var exampleSurname = new string[] { "Valdez", "Ingram", "Cannon", "Herman", "Blanchard", "Wilder", "Matthews", "Livingston", "Stein", "Dodson", "Valdez", "Frederick", "Durham", "Cantu", "Beard", "Cruz", "Barnes", "Clayton", "Gregory", "Puckett", "Burris", "Mullins", "King", "Webster", "Rogers", "Ray", "Winters", "Hopkins", "Warner", "Chaney", "Castaneda", "Hopkins", "Benjamin", "Estes", "Everett", "Carlson", "Mccarty", "Soto", "Hines", "Camacho", "Booth", "Macias", "Yates", "Daugherty", "Sheppard", "Leon", "Mcguire", "Blankenship", "Howard", "Kemp", "Kelley", "Little", "Greer", "Morgan", "Farmer", "Brewer", "Knight", "Savage", "Blevins", "Skinner", "Foley", "Lang", "Lancaster", "Schwartz", "Watts", "Hanson", "Rivas", "Shepherd", "Zimmerman", "Davis", "Ray", "Green", "Kaufman", "Vance", "West", "Hurley", "Robertson", "Roth", "Stanton", "Mcintosh", "Howard", "Dudley", "Levy", "Watson", "Sears", "Ward", "Daniels", "Conley", "Singleton", "Tucker", "Howell", "Lopez", "Coffey", "Butler", "Campbell", "Ross", "Schroeder", "Maddox", "Stephenson", "Rowland", "Dennis", "Obrien", "Strickland", "Rios", "Mason", "Rich", "Orr", "House", "Fernandez", "Martinez", "Mcintyre", "Moore", "Harvey", "Brock", "Leonard", "Lloyd", "Trujillo", "Briggs", "Wallace", "Oneal", "Slater", "Madden", "Turner", "Craig", "Finley", "Higgins", "Sargent", "French", "Kline", "Burns", "Wilson", "Mullins", "Riggs", "Nash", "Moreno", "Holder", "Pickett", "Barrera", "Slater", "Hicks", "Riddle", "Booth", "Maynard", "Nieves", "Huff", "Mclean", "Wilder", "Edwards", "Hatfield", "Sherman", "Singleton", "Browning", "Bolton", "Lynn", "Conner", "Byers", "Mann", "Bradshaw", "House", "Shaw", "Mccall", "Marquez", "Puckett", "Case", "Carlson", "Bates", "Patel", "Lyons", "Crawford", "Cochran", "Armstrong", "Snider", "Compton", "Kent", "Gutierrez", "Moran", "Perez", "Hammond", "Cherry", "Allison", "Sutton", "Miranda", "Wilkins", "Chan", "Perkins", "Navarro", "Miles", "Mueller", "England", "Davis", "Klein", "Travis", "Mckenzie", "Stuart", "Donaldson", "Nolan", "Terrell", "Evans", "Wiggins", "Jones", "Mejia", "Logan", "Barnett", "Foley", "Walters", "Molina", "Justice", "Melendez", "Henry", "Gates", "Leonard", "Stein", "Kirby", "Pace", "Deleon", "Pitts", "Ashley", "Murray", "Garner", "Dominguez", "Hebert", "Brock", "Hurst", "Berry", "Nixon", "Velez", "Blackwell", "Turner", "Elliott", "Haynes", "Hodge", "Saunders", "Barnes", "Vance", "Oconnor", "Kramer", "Mason", "Robbins", "Campos", "Everett", "Morton", "Hancock", "Colon", "Olsen", "Heath", "Ferguson", "Rojas", "Hopper", "Saunders", "Noble", "Silva", "Benton", "Ware", "French", "Bryan", "Bass", "Swanson", "Roberts", "Castro", "Horn", "Wagner", "Morgan", "Montgomery", "Mcmahon", "Rowe", "Maxwell", "Collier", "Lawson", "Hopkins", "York", "Levine", "Dillon", "Mcfarland", "Burch", "Vega", "Hancock", "Flowers", "Walter", "Calhoun", "Rivas", "Ochoa", "Burns", "Crane", "Michael", "Ray", "Savage", "Herring", "Steele", "Serrano", "Manning", "Wilkerson", "Mercado", "Irwin", "Jordan", "Knight", "Sykes", "Mckay", "Burks", "Church", "Underwood", "Peters", "Chaney", "Bruce", "Bradley", "Avery", "Durham", "Gonzalez", "Johns", "Davidson", "Miranda", "Strickland", "Travis", "Martinez", "Lynn", "Oliver", "Simmons", "Tillman", "Walker", "Frazier", "Morgan", "Merritt", "Boone", "Willis", "French", "Woodward", "Mays", "Glass", "Hendricks", "Hull", "Rice", "Rivers", "Mccray", "Potts", "Mills", "Chandler", "Blackwell", "Mann", "Dodson", "Golden", "Shepherd", "Foster", "Nixon", "Allen", "Valencia", "Clarke", "Warner", "Rollins", "Little", "Wooten", "Cook", "Ramsey", "William", "Dyer", "Herman", "Anthony", "Sparks", "Pollard", "Jacobs", "Gregory", "Simpson", "Young", "Camacho", "Roy", "Green", "Anthony", "Poole", "Cortez", "Rodriguez", "Hayes", "Ferrell", "Gregory", "Mccarthy", "Wiley", "Kelley", "Gallagher", "Short", "Abbott", "Humphrey", "Boyle", "Fitzpatrick", "Beck", "Reeves", "Buchanan", "Mills", "Montoya", "Wells", "Day", "Shepherd", "Berg", "Fleming", "Barnes", "Ray", "Kirkland", "Young", "Lopez", "Parsons", "Hyde", "Morton", "Palmer", "Brooks", "Leonard", "Shelton", "Combs", "Terrell", "Madden", "Moran", "Howe", "Green", "Hess", "Chen", "Mcmahon", "Lawrence", "Beard", "Decker", "Cannon", "Cotton", "Cotton", "Baxter", "Hopkins", "Trevino", "Langley", "Gillespie", "Phelps", "Hays", "Reynolds", "Graves", "Stevens", "Lewis", "Powell", "Fisher", "Strong", "Walton", "Suarez", "Conrad", "Harding", "Becker", "Pickett", "Strickland", "Mccormick", "Harmon", "Farrell", "Sharp", "Hicks", "Patterson", "Ayala", "Vincent", "Sosa", "Hewitt", "Lawrence", "Ross", "Fry", "Floyd", "Berry", "Drake", "Sampson", "Carr", "Macdonald", "Turner", "Nunez", "Hayden", "Vaughan", "Maldonado", "Horton", "Bishop", "Walter", "Garner", "Hunt", "Harvey", "Grimes", "Williamson", "Greene", "Dodson", "Reed", "Walton", "Mcintosh", "Hensley", "Spence", "Mays", "Stout", "Bauer", "Chaney", "Robbins", "Stein", "Ruiz", "Barrett", "Mccullough", "Clark", "Jenkins", "Hatfield", "Garner", "Harmon", "Key", "Juarez", "Winters", "Barber", "Salazar", "Conway", "Huber", "Roach", "Paul" };
            return exampleSurname[new Random().Next(0, exampleSurname.Length)];
        }
    }
}
