using System;
using System.IO;

namespace Stealthy.Persistence
{
    public class StealthyDataAccess : IStealthyDataAcces
    {
        #region Read Table
        public StealthyTable Load(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path)) 
                {
                    String line = reader.ReadLine() ?? String.Empty;
                    String[] numbers; // = line.Split(' '); // read one row and split by spaces
                    Int32 tableSize = Int32.Parse(line); // read the table size
                    StealthyTable table = new StealthyTable(tableSize); // create the table

                    for (Int32 i = 0; i < tableSize; ++i)
                    {
                        line = reader.ReadLine() ?? String.Empty;
                        numbers = line.Split(' ');

                        for (Int32 j = 0; j < tableSize; j++)
                        {
                            switch (numbers[j])
                            {
                                case "J":
                                    table.FieldValue(i, j, FieldElement.PLAYER);
                                    break;
                                case "O":
                                    table.FieldValue(i, j, FieldElement.GUARD);
                                    break;
                                case "P":
                                    table.FieldValue(i, j, FieldElement.FLOOR);
                                    break;
                                case "F":
                                    table.FieldValue(i, j, FieldElement.WALL);
                                    break;
                                case "K":
                                    table.FieldValue(i, j, FieldElement.EXIT);
                                    break;
                                default:
                                    throw new Exception("Incorrect field data");
                            }
                        }
                    }
                    return table;
                }
            }
            catch(IndexOutOfRangeException e)
            {
                throw e;
            }
            catch(Exception )
            {
                throw new StealthyDataException();
            }
        }
        #endregion
    }
}
