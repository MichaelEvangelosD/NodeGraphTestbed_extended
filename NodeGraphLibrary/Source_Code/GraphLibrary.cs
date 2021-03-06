using System.Text;

namespace NodeGraphLibrary
{
    /// <summary>
    /// A class library containing all the necessary methods to manage an undirected graph - uses arrays.
    /// </summary>
    public static class GraphLibrary
    {
        static string[] vertexes = new string[5];
        static string[,] vertexLinks = new string[6, 2];

        /// <returns>The vertexes array</returns>
        public static string[] GetVertexArray()
        {
            return vertexes;
        }

        /// <returns>The vertexes array</returns>
        public static string[,] GetEdgesArray()
        {
            return vertexLinks;
        }

        #region NODE_CREATION
        /// <summary>
        /// Call to create a new vertex with the given name to the next empty array position
        /// </summary>
        /// <param name="vertexName">The name of the vertex</param>
        /// <returns>True if the vertex was succesfully created.
        /// <para>False is there was no space inside the array.</para></returns>
        public static bool AddVertexToArray(string vertexName)
        {
            string simplifiedName = vertexName.Replace(" ", "").ToLower();

            int index = GetArrayEmptyIndex(vertexes);

            if (index != -1 && index >= 0)
            {
                vertexes[index] = simplifiedName;

                //If we successfully created the vertex
                return true;
            }

            //If the vertex list is full...
            return false;
        }
        #endregion

        #region NODE_CONNECTING
        /// <summary>
        /// Call to create a vertex between vertexNameA and vertexNameB.
        /// </summary>
        /// <returns>A string "Connected x with y"</returns>
        public static string CreateVertexEdge(string vertexNameA, string vertexNameB)
        {
            //Search for an empty space in the vertexes array
            for (int i = 0; i < vertexLinks.GetLength(0); i++)
            {
                //If the i position is empty - then both positions are empty...
                if (vertexLinks[i, 0] == null)
                {
                    //...set the new connection entry
                    vertexLinks[i, 0] = vertexNameA;
                    vertexLinks[i, 1] = vertexNameB;

                    string returnSentence = $"Connected {vertexNameA} with {vertexNameB}";
                    return returnSentence;
                }
            }

            return $"Could not connect {vertexNameA} with {vertexNameB}";
        }
        #endregion

        #region VERTEX_DELETION
        /// <summary>
        /// Call to cycle through the given array and set BOTH entries of the given name 
        /// inside the array to null
        /// </summary>
        /// <param name="name">The name to delete.</param>
        /// <param name="edgesArray">The array to search for the name in.</param>
        /// <returns>A string containing the deleted connections.</returns>
        public static string StartEdgeDeletionSequence(string name, string[,] edgesArray)
        {
            StringBuilder tempSB = new StringBuilder();

            for (int i = 0; i < edgesArray.GetLength(0); i++)
            {
                //If the current index is null then continue to the next one
                if (edgesArray[i, 0] == null) continue;

                //If the i index matches the given name then delete both array entries
                if (edgesArray[i, 0] == name || edgesArray[i, 1] == name)
                {
                    tempSB.AppendLine($"Deleted connection {edgesArray[i, 0]}-{edgesArray[i, 1]} from the connection list.\n");

                    DeleteVertexFromArray(edgesArray, i);
                }
            }

            return tempSB.ToString();
        }
        #endregion

        #region UTILITIES
        //FULLNESS CHECK
        /// <summary>
        /// Call to check if there are any null spaces in the vertexes array
        /// </summary>
        /// <returns>False if there is a null space
        /// <para>True if not</para></returns>
        public static bool IsVertexArrayFull()
        {
            foreach (string item in vertexes)
            {
                if (item == null) return false;
            }

            return true;
        }

        /// <summary>
        /// Call to check if there is a null space in the 1st dimension of the edges array.
        /// </summary>
        /// <returns>False if there is a null space
        /// <para>True if not</para></returns>
        public static bool IsEdgesArrayFull()
        {
            for (int i = 0; i < vertexLinks.GetLength(0); i++)
            {
                if (vertexLinks[i, 0] == null) return false;
            }

            return true;
        }

        //VERTEX EXISTANCE and CONNECTION CHECK
        /// <summary>
        /// Call to search the vertexes array for the given name
        /// </summary>
        /// <param name="name">Name to search for.</param>
        /// <returns>True if name is found, false otherwise</returns>
        public static bool SearchVertexNameExistance(string name)
        {
            string simplifiedName = name.Replace(" ", "").ToLower();

            for (int i = 0; i < vertexes.Length; i++)
            {
                if (simplifiedName == vertexes[i])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Call to compare the vertex and edge linking to the edges array.
        /// </summary>
        /// <returns>True is the linking DOES exist,
        /// <para>False otherwise.</para></returns>
        public static bool CompareVertexEdge(string vertexName, string edgeName)
        {
            for (int i = 0; i < vertexLinks.GetLength(0); i++)
            {
                if ((vertexLinks[i, 0] == vertexName && vertexLinks[i, 1] == edgeName)
                    || (vertexLinks[i, 0] == edgeName && vertexLinks[i, 1] == vertexName))
                {
                    return true;
                }
            }

            return false;
        }

        //ARRAY EMPTY INDEX CHECK
        /// <summary>
        /// <para>INTERNAL USE ONLY</para>
        /// Call to find the first null space in the given array.
        /// </summary>
        /// <returns>The position of the empty space.
        /// <para>-1 if no empty space was found.</para></returns>
        static int GetArrayEmptyIndex(string[] arrayToCheck)
        {
            for (int i = 0; i < arrayToCheck.Length; i++)
            {
                if (arrayToCheck[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// <para>INTERNAL USE ONLY</para>
        /// Call to find the first null space in the 1st dimension of the given array.
        /// </summary>
        /// <returns>The position of the empty space
        /// <para>-1 if no empty space was found</para></returns>
        static int GetArrayEmptyIndex(string[,] arrayToCheck)
        {
            for (int i = 0; i < arrayToCheck.GetLength(0); i++)
            {
                if (arrayToCheck[i, 0] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        //DELETION METHODS
        /// <summary>
        /// Call to set the given index inside the given array to NULL
        /// </summary>
        /// <param name="vertArray">The array to modify.</param>
        /// <param name="index">The array index to set to null.</param>
        public static void DeleteVertexFromArray(string[] vertArray, int index)
        {
            vertArray[index] = null;
        }

        /// <summary>
        /// Call to set the [index,0] and [index,1] array positions to NULL.
        /// </summary>
        /// <param name="vertArray">The 2D array to modify</param>
        /// <param name="index">The array index to set to null.</param>
        public static void DeleteVertexFromArray(string[,] vertArray, int index)
        {
            for (int i = 0; i < vertArray.GetLength(1); i++)
            {
                vertArray[index, i] = null;
            }
        }
        #endregion
    }
}