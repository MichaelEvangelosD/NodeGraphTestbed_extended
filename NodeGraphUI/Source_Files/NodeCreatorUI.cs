using System;
using NodeGraphLibrary;

namespace NodeGraphUI
{
    class NodeCreatorUI
    {
        static void Main(string[] args)
        {
            new NodeCreatorUI().Execute();
        }

        /// <summary>
        /// Execute the program
        /// </summary>
        void Execute()
        {
            string answer = "";
            int result;

            while (!answer.Equals("0"))
            {
                DisplayMenu();
                PrintSeparatorLines();

                Console.Write("Choice: ");
                answer = Console.ReadLine();

                //Try parsing the user choice into an int
                if (Int32.TryParse(answer, out result))
                {
                    //...and check if it is a valid choice
                    SwitchOnAnswer(result);
                }
            }
        }

        /// <summary>
        /// Display selection menu
        /// </summary>
        void DisplayMenu()
        {
            Console.WriteLine("Please select an option" +
                "\n 1) Create station" +
                "\n 2) Create station connection" +
                "\n 3) Print all station connections" +
                "\n 4) Delete a station" +
                "\n 5) Delete a station connection" +
                "\n 0) Terminate program");
        }

        /// <summary>
        /// Call to switch on the user menu choice
        /// </summary>
        /// <param name="selection">The user-selected number</param>
        void SwitchOnAnswer(int selection)
        {
            switch (selection)
            {
                case 1: //Create a new vertex
                    CreateVertexSequence();

                    PrintSeparatorLines();
                    break;
                case 2: //Create a new edge between vertexes
                    AskVertexLinkNames();

                    PrintSeparatorLines();
                    break;
                case 3: //Display the edges between vertexes
                    PrintSeparatorLines();

                    DisplayVertexLinking();

                    PrintSeparatorLines();
                    break;
                case 4: //Prompt the user to delete a vertex
                    DeleteVertex();

                    PrintSeparatorLines();
                    break;
                case 5: //Prompt the user to delete a vertex edge
                    DeleteVertexEdge();

                    PrintSeparatorLines();
                    break;
                case 0: //Terminate program
                    Environment.Exit(1);
                    break;
                default: //When none of the above is selected
                    PrintColoredMessage(ConsoleColor.Red, "Please select a valid option.");
                    break;
            }
        }

        //COMPLETED
        #region NODE_CREATION
        /// <summary>
        /// Call to ask the user to provide a new node name and check if it exists inside the nodes array.
        /// If it doesn't exist then add it to the nodes array.
        /// </summary>
        void CreateVertexSequence()
        {
            //Early exit if the vertex list is already full
            if (GraphLibrary.IsVertexArrayFull())
            {
                PrintColoredMessage(ConsoleColor.Red, "Station list is full");
                return;
            }

            //Prompt for vertex name
            string givenName;
            bool namesIsValid = false;

            do
            {
                Console.Write("Station name: ");
                givenName = Console.ReadLine();

                if (GraphLibrary.SearchVertexNameExistance(givenName))
                {
                    PrintColoredMessage(ConsoleColor.Cyan, $"Station name {givenName} already exists!");
                }
                else
                {
                    namesIsValid = true;
                }

            } while (!namesIsValid);

            //Create and add node to array
            if (GraphLibrary.AddVertexToArray(givenName))
            {
                PrintColoredMessage(ConsoleColor.Green, $"Station {givenName} created");
            }
        }
        #endregion

        //WIP
        #region NODE_CONNECTING
        void AskVertexLinkNames()
        {
            //Early exit if the edges array is already full
            if (GraphLibrary.IsEdgesArrayFull())
            {
                PrintColoredMessage(ConsoleColor.Red, "The connections list is full!");
                return;
            }

            string vertexName, edgeName;

            Console.WriteLine("Connect which station ?");
            vertexName = Console.ReadLine();

            Console.WriteLine($"Connect {vertexName} to ...?");
            edgeName = Console.ReadLine();

            PrintColoredMessage(ConsoleColor.Red, "NODE_CONNECTING is WIP");
            //GraphLibrary.TryCreateVertexEdge(vertexName, edgeName);
        }
        #endregion

        //COMPLETED
        #region NODE_PRINTING
        /// <summary>
        /// Call to write all the contents of the vertexes array into the console
        /// </summary>
        void DisplayVertexesArray()
        {
            for (int i = 0; i < GraphLibrary.GetVertexArray().Length; i++)
            {
                Console.WriteLine($"Station {i}: {GraphLibrary.GetVertexArray()[i]}");
            }
        }

        /// <summary>
        /// Call to display all node connections from the nodeConnections array.
        /// </summary>
        static void DisplayVertexLinking()
        {
            Console.WriteLine("|Stations|\t|Connections|");

            for (int i = 0; i < GraphLibrary.GetEdgesArray().GetLength(0); i++)
            {
                for (int j = 0; j < GraphLibrary.GetEdgesArray().GetLength(1); j++)
                {
                    Console.Write($"{i} {GraphLibrary.GetEdgesArray()[i, j]}\t");
                }
                Console.WriteLine();
            }
        }
        #endregion

        //COMPLETED
        #region NODE_DELETION
        /// <summary>
        /// Call to display the nodes array and prompt the user which one to delete.
        /// <para>Marks the given array index as null</para>
        /// </summary>
        void DeleteVertex()
        {
            //Helper variables
            string userChoice, deletedEdgesInfo;
            int parsedAnswer;

            //Ask the user to delete a node at least once
            do
            {
                PrintSeparatorLines();

                Console.WriteLine("Which station to delete?");
                DisplayVertexesArray(); //Display the vertex array

                //Cache and parse the user input
                userChoice = Console.ReadLine();

            } while (!Int32.TryParse(userChoice, out parsedAnswer));

            //Check if the given answer is inside the bounds of the array
            if (parsedAnswer >= 0 && parsedAnswer <= GraphLibrary.GetVertexArray().Length)
            {
                //Cache the vertex name before it gets deleted so we can use it 
                //to search the edges array for the connections
                string cachedVertexName = GraphLibrary.GetVertexArray()[parsedAnswer];

                //Nullify the given index position inside the vertexes array
                GraphLibrary.DeleteVertexFromArray(GraphLibrary.GetVertexArray(), parsedAnswer);

                deletedEdgesInfo = GraphLibrary.SearchEdgeNameAndDelete(cachedVertexName, GraphLibrary.GetEdgesArray());

                PrintColoredMessage(ConsoleColor.Red, deletedEdgesInfo);

                PrintColoredMessage(ConsoleColor.Yellow, $"Deleted station from entry position number : {userChoice}");
            }
            else
            {
                PrintColoredMessage(ConsoleColor.Red, "Given number is out of bounds.");
            }
        }

        /// <summary>
        /// Call to prompt the user to delete a node connection from inside the nodeConnections array.
        /// <para>If it exists both array indexes get nullified.</para>
        /// </summary>
        void DeleteVertexEdge()
        {
            //Helper variables
            string userChoice;
            int parsedAnswer;

            //Ask the user to delete a connection at least once
            do
            {
                PrintSeparatorLines();

                Console.WriteLine("Which connection to delete?");
                DisplayVertexLinking(); //Display the vertex edges array

                //Cache and parse the user input
                userChoice = Console.ReadLine();
            } while (!Int32.TryParse(userChoice, out parsedAnswer));

            //Check if the given answer is inside the bounds of the array
            if (parsedAnswer >= 0 && parsedAnswer <= GraphLibrary.GetEdgesArray().GetLength(0))
            {
                //Clear both array entries
                GraphLibrary.DeleteVertexFromArray(GraphLibrary.GetEdgesArray(), parsedAnswer);

                PrintColoredMessage(ConsoleColor.Yellow, $"Deleted connection from entry position number : {userChoice}");
            }
            else
            {
                PrintColoredMessage(ConsoleColor.Red, "Given number is out of bounds.");
            }
        }
        #endregion

        #region UTILITIES
        /// <summary>
        /// Call to WriteLine the message in the console with the given color,
        /// <para>color is set back to white after the message gets printed.</para>
        /// </summary>
        /// <param name="cColor">Color of the given sentence</param>
        /// <param name="message">The message to display</param>
        void PrintColoredMessage(ConsoleColor cColor, string message)
        {
            Console.ForegroundColor = cColor;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Call to write 50 '-' to the console for STYLE
        /// </summary>
        void PrintSeparatorLines()
        {
            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine();
        }
        #endregion
    }
}
