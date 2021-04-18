using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FSMManager : MonoBehaviour
{
    // Variables
    
    // Lists
    public List<GameObject> mapList = new List<GameObject>();
    public List<GenomeDataClass> currentGenerationGenomeArray = new List<GenomeDataClass>();
    public List<GenomeDataClass> nextGenerationGenomeArray = new List<GenomeDataClass>();
    public List<GenomeDataClass> bestSeedMarioGenomeArray = new List<GenomeDataClass>();

    // Map to be istantiated
    public GameObject mapPrefab;
    // Parent object maps spawn under
    public Transform parentGrid;

    // The width and height of the map used to instantiate other maps
    public float xBound;
    public float yBound;

    // How many are in the grid: 1X1 or 2X1 etc
    public int gridSizeX;
    public int gridSizeY;

    // Where you want the instantiation to start.
    public Vector2 originPos;

    // Genetic Algorithm Variables
    [Header("Genetic Algorithm Variables")]
    private int numberOfMarios;
    public GenomeDataClass firstParent;
    public GenomeDataClass secondParent;
    public int mutationChance = 15;
    public int generation = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Number of marios is grid length times grid height
        numberOfMarios = gridSizeX * gridSizeY;

        InstantiateMaps();
    }

    // Update is called once per frame
    void Update()
    {
        // If current generation saved in list is equal to the total number of marios
        // This only runs once all the marios have either completed the level or died
        if (currentGenerationGenomeArray.Count == numberOfMarios)
        {
            // Sort the genome list to have best first 
            SortGenome();

            // Clear and prep nextgenerationArray
            nextGenerationGenomeArray.Clear();

            for (int i=0; i < currentGenerationGenomeArray.Count; i++)
            {
                
                // If best seed save to best seed array
                if (i == 0)
                {
                    bestSeedMarioGenomeArray.Add(currentGenerationGenomeArray[i]);
                }

                // Save to next generation array for Genetic Alogirthm functions
                nextGenerationGenomeArray.Add(currentGenerationGenomeArray[i]);              
            }

            // Run Selection
            Selection();

            // Clear maps
            ClearMaps();

            // Instate maps
            InstantiateMaps();

            // Move to new Gen
            generation++;

            // Clear current gen once not needed for next generation
            currentGenerationGenomeArray.Clear();
        }
    }

    private void SortGenome()
    {
        // Sort the list from lowest to highest
        currentGenerationGenomeArray = currentGenerationGenomeArray.OrderBy(fitness => fitness.genomeFitnessScore).ToList();

        // Reverse list so best is first
        currentGenerationGenomeArray.Reverse();
    }

    private void Selection()
    {
        // Sets count for iteration
        int count = nextGenerationGenomeArray.Count();

        // Iterate through the sorted next gen and remove the worst 24
        nextGenerationGenomeArray.RemoveRange(26, 24);

        // Iterate through the shortened list and choose who breed
        // Worst 1 of remaining 26 wont breed.
        for (int i = 0; i < 24; i++)
        {
            // Breed current with next parent
            Crossover(i, i + 1);
        }
    }
    
    public void Crossover(int firstParentIndex, int secondParentIndex)
    {
        // Set parents based on index
        firstParent = nextGenerationGenomeArray[firstParentIndex];
        secondParent = nextGenerationGenomeArray[secondParentIndex];

        // Create temps for temp storage
        float tempDelayedTime = 0;
        float tempForwardRayLength = 0;
        float tempUpRightRayLength = 0;
        float tempDownRightRayLength = 0;
        float tempPreferedForwardRayLength = 0;
        float tempPreferedUpRightRayLength = 0;
        float tempPreferedDownRightRayLength = 0;

        float chance;

        // for each gene
        for (int i = 0; i < 7; i++)
        {
            // Pick which parent to take gene from
            if (i == 0)
            {
                // Generate chance
                chance = GenerateRandomNumber(1, 100);

                // Pick which parent to take gene from
                if (chance > 50)
                {
                    tempDelayedTime = firstParent.GetDelayedTime();
                }
                else
                {
                    tempDelayedTime = secondParent.GetDelayedTime();
                }

                // Check for mutation and mutate accordingly
                if (CheckForMutation())
                {
                    tempDelayedTime = Mutate(tempDelayedTime, 3);
                }

            }
            // Pick which parent to take gene from
            if (i == 1)
            {
                // Generate chance
                chance = GenerateRandomNumber(1, 100);

                // Pick which parent to take gene from
                if (chance > 50)
                {
                    tempForwardRayLength = firstParent.GetForwardRaycastLength();
                }
                else
                {
                    tempForwardRayLength = secondParent.GetForwardRaycastLength();
                }

                // Check for mutation and mutate accordingly
                if (CheckForMutation())
                {
                    tempForwardRayLength = Mutate(tempForwardRayLength, 10);
                }
            }
            // Pick which parent to take gene from
            if (i == 2)
            {
                // Generate chance
                chance = GenerateRandomNumber(1, 100);

                // Pick which parent to take gene from
                if (chance > 50)
                {
                    tempUpRightRayLength = firstParent.GetForwardUpRaycastLength();
                }
                else
                {
                    tempUpRightRayLength = secondParent.GetForwardUpRaycastLength();
                }

                // Check for mutation and mutate accordingly
                if (CheckForMutation())
                {
                    tempUpRightRayLength = Mutate(tempUpRightRayLength, 10);
                }
            }
            // Pick which parent to take gene from
            if (i == 3)
            {
                // Generate chance
                chance = GenerateRandomNumber(1, 100);

                // Pick which parent to take gene from
                if (chance > 50)
                {
                    tempDownRightRayLength = firstParent.GetForwardDownRaycastLength();
                }
                else
                {
                    tempDownRightRayLength = secondParent.GetForwardDownRaycastLength();
                }

                // Check for mutation and mutate accordingly
                if (CheckForMutation())
                {
                    tempDownRightRayLength = Mutate(tempDownRightRayLength, 10);
                }
            }
            // Pick which parent to take gene from
            if (i == 4)
            {
                // Pick which parent to take gene from
                if (tempForwardRayLength == firstParent.GetForwardRaycastLength())
                {
                    tempPreferedForwardRayLength = firstParent.GetPreferedForwardRaycastLength();
                }
                else
                {
                    tempPreferedForwardRayLength = secondParent.GetPreferedForwardRaycastLength();
                }

                // Check for mutation and mutate accordingly
                if (CheckForMutation())
                {
                    tempPreferedForwardRayLength = Mutate(tempPreferedForwardRayLength, tempForwardRayLength);
                }
            }
            // Pick which parent to take gene from
            if (i == 5)
            {
                // Pick which parent to take gene from
                if (tempUpRightRayLength == firstParent.GetForwardUpRaycastLength())
                {
                    tempPreferedUpRightRayLength = firstParent.GetPreferedForwardUpRaycastLength();
                }
                else
                {
                    tempPreferedUpRightRayLength = secondParent.GetPreferedForwardUpRaycastLength();
                }

                // Check for mutation and mutate accordingly
                if (CheckForMutation())
                {
                    tempPreferedUpRightRayLength = Mutate(tempPreferedUpRightRayLength, tempUpRightRayLength);
                }
            }
            // Pick which parent to take gene from
            if (i == 6)
            {
                // Pick which parent to take gene from
                if (tempDownRightRayLength == firstParent.GetForwardDownRaycastLength())
                {
                    tempPreferedDownRightRayLength = firstParent.GetPreferedForwardDownRaycastLength();
                }
                else
                {
                    tempPreferedDownRightRayLength = secondParent.GetPreferedForwardDownRaycastLength();
                }

                // Check for mutation and mutate accordingly
                if (CheckForMutation())
                {
                    tempPreferedDownRightRayLength = Mutate(tempPreferedDownRightRayLength, tempDownRightRayLength);
                }
            }
        }

        // Set up new child
        GenomeDataClass newChild = new GenomeDataClass(tempDelayedTime, 
                                                        tempForwardRayLength, 
                                                        tempUpRightRayLength, 
                                                        tempDownRightRayLength, 
                                                        tempPreferedForwardRayLength, 
                                                        tempPreferedUpRightRayLength,
                                                        tempPreferedDownRightRayLength);

        
        // Add the new child to the next generation
        nextGenerationGenomeArray.Add(newChild);
    }

    public bool CheckForMutation()
    {
        // Generate a percentage chance
        int chance = Mathf.RoundToInt(Random.Range(1, 100));

        // See if mutation occurs
        if (chance <= mutationChance)
        {
            // Mutation occurs
            return true;
        }
        else
        {
            // Mutation doesn't occur
            return false;
        }
    }

    public float Mutate(float value, float defaultMax)
    {
        // Generate percentage chance
        float chance = GenerateRandomNumber(1, 100);

        // If mutates randomly
        if (chance <= 33)
        {
            // Set value to random value within range
            value = GenerateRandomNumber(0, defaultMax);
        }
        // If mutates addition
        else if (chance > 33 && chance <= 66)
        {
            // Find max possible addition
            float maxRange = defaultMax - value;

            // If something can be added
            if (maxRange > 0)
            {
                // Generate addition
                float addition = Random.Range(0, maxRange);

                // Add to value
                value += addition;
            }
            // If the value is already at maximum
            else
            {
                // Set to max
                value = defaultMax;
            }
        }
        // If mutates subtraction
        else
        {
            // Find max possible subtraction
            float maxRange = value;

            // If something can be subtracted
            if (maxRange > 0.01f)
            {
                // Generate subtraction
                float subtraction = Random.Range(0, maxRange);

                // Subtract from value
                value -= subtraction;
            }
            // If the value is already at minimum
            else
            {
                // Set value to 1
                value = 0.01f;
            }
        }

        // Return Value
        return value;
    }

    public float GenerateRandomNumber(float startNumber, float endNumber)
    {
        float randomVariable = Mathf.RoundToInt(Random.Range(startNumber, endNumber));

        return randomVariable;
    }

    private void ClearMaps()
    {
        GameObject item;
        
        // Iterate through maps
        for (int i = 0; i < mapList.Count; i++)
        {
            if (item = null)
            {
                item = mapList[i];
            }
           

            Destroy(mapList[i].gameObject);
        }

        // Clear Map List
        mapList.Clear();
    }

    private void InstantiateMaps()
    {
        // Gets the tilemap of the level for calculations
        Tilemap tilemap = mapPrefab.GetComponent<Tilemap>();
        var bounds = tilemap.cellBounds;
        xBound = (Mathf.Abs(bounds.xMax) + Mathf.Abs(bounds.xMin));
        yBound = (Mathf.Abs(bounds.yMax) + Mathf.Abs(bounds.yMin));

        // For each grid cell
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                // Instantiate level
                Vector2 pos = new Vector2(originPos.x + (x * xBound), originPos.y + (y * yBound));
                var newMap = Instantiate(mapPrefab, pos, Quaternion.identity);
                newMap.transform.parent = parentGrid;
                mapList.Add(newMap);
            }
        }
        for(int i = 0; i < mapList.Count; i++)
        {
            mapList[i].GetComponent<LevelScript>().SpawnMario();
        }

       try
       {
            for (int i = 0; i < mapList.Count; i++)
            {
                Debug.Log(nextGenerationGenomeArray[i].genomeToString());
                mapList[i].GetComponent<LevelScript>().LoadMarioGenome(nextGenerationGenomeArray[i]);
            }
       }
       catch
       {
            Debug.Log("Check Generation");
       }        
    }

    public void SaveGenomeToXMLFile(string fileName)
    {
        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;

        // Create a writing instance
        XmlWriter xmlGenomeWriter = XmlWriter.Create(fileName + ".xml", writerSettings);

        // Write the beginning of the Document
        xmlGenomeWriter.WriteStartDocument();

        // Write the root element
        xmlGenomeWriter.WriteStartElement("GenomeLibrary");

        // itterate through the array of genomes
        for (int i =0; i < bestSeedMarioGenomeArray.Count; i++)
        {
            // Create a single Genome Element
            xmlGenomeWriter.WriteStartElement("Genome");

            // Add the attribute of which generation is being written
            xmlGenomeWriter.WriteAttributeString("Generation",  i.ToString());

            // Create delayed time element
            xmlGenomeWriter.WriteStartElement("DelayedTime");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetDelayedTime().ToString());

            // End the delayed time element
            xmlGenomeWriter.WriteEndElement();

            // Create forward raycast length element
            xmlGenomeWriter.WriteStartElement("ForwardRaycastLength");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetForwardRaycastLength().ToString());

            // End the forward raycast length element
            xmlGenomeWriter.WriteEndElement();

            // Create forward up raycast length element
            xmlGenomeWriter.WriteStartElement("ForwardUpRaycastLength");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetForwardUpRaycastLength().ToString());

            // End the forward up raycast length element
            xmlGenomeWriter.WriteEndElement();

            // Create forward down raycast length element
            xmlGenomeWriter.WriteStartElement("ForwardDownRaycastLength");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetForwardDownRaycastLength().ToString());

            // End the forward down raycast length element
            xmlGenomeWriter.WriteEndElement();

            // Create forward raycast length element
            xmlGenomeWriter.WriteStartElement("PreferedForwardRaycastLength");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetPreferedForwardRaycastLength().ToString());

            // End the forward raycast length element
            xmlGenomeWriter.WriteEndElement();

            // Create forward up raycast length element
            xmlGenomeWriter.WriteStartElement("PreferedForwardUpRaycastLength");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetPreferedForwardUpRaycastLength().ToString());

            // End the forward up raycast length element
            xmlGenomeWriter.WriteEndElement();

            // Create forward down raycast length element
            xmlGenomeWriter.WriteStartElement("PreferedForwardDownRaycastLength");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetPreferedForwardDownRaycastLength().ToString());

            // End the forward down raycast length element
            xmlGenomeWriter.WriteEndElement();

            // Create fitness score length element
            xmlGenomeWriter.WriteStartElement("FitnessScore");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetFitnessScore().ToString());

            // End the fitness score element
            xmlGenomeWriter.WriteEndElement();

            // End Genome Element
            xmlGenomeWriter.WriteEndElement();
        }

        // End root element
        xmlGenomeWriter.WriteEndElement();

        // Write end of document
        xmlGenomeWriter.WriteEndDocument();

        // Close the document to save
        xmlGenomeWriter.Close();
    }
}