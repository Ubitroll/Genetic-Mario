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
    List<GameObject> mapList;

    
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

    // File String
    [Header("XML FILE WRITING VARIABLE")]
    public string genomeFileName = "GenomeFile";

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
        numberOfMarios = gridSizeX * gridSizeX;

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

    }
    
    public void Crossover(int firstParentIndex, int secondParentIndex)
    {
        // Set parents based on index
        firstParent = nextGenerationGenomeArray[firstParentIndex];
        secondParent = nextGenerationGenomeArray[secondParentIndex];

        // Create temps for temp storage
        int tempDelayedThreshold = 0;
        int tempLongThreshold = 0;
        int tempDelayedTime = 0;
        int tempForwardRayLength = 0;
        int tempUpRightRayLength = 0;
        int tempDownRightRayLength = 0; 

        int chance;

        // for each gene
        for (int i = 0; i < 6; i++)
        {
            // Pick which parent to take gene from
            if (i == 0)
            {
                // Check if mutates
                if (CheckForMutation())
                {
                    // If mutates
                    tempDelayedThreshold = GenerateRandomNumber(1, 100);
                }
                // If not crossover like normal
                else
                {
                    // Generate chance
                    chance = GenerateRandomNumber(1 , 100);

                    // Pick which parent to take gene from
                    if (chance > 50)
                    {
                        tempDelayedThreshold = firstParent.GetDelayedThreshold();
                    }
                    else
                    {
                        tempDelayedThreshold = secondParent.GetDelayedThreshold();
                    }
                }
            }
            // Pick which parent to take gene from
            if (i == 1)
            {
                // Check if mutates
                if (CheckForMutation())
                {
                    // If mutates
                    tempLongThreshold = GenerateRandomNumber(1, 100);
                }
                // If not crossover like normal
                else
                {
                    // Generate chance
                    chance = GenerateRandomNumber(1, 100);

                    // Pick which parent to take gene from
                    if (chance > 50)
                    {
                        tempLongThreshold = firstParent.GetLongJumpThreshold();
                    }
                    else
                    {
                        tempLongThreshold = secondParent.GetLongJumpThreshold();
                    }
                }
            }
            // Pick which parent to take gene from
            if (i == 2)
            {
                // Check if mutates
                if (CheckForMutation())
                {
                    // If mutates
                    tempDelayedTime = GenerateRandomNumber(0, 3);
                }
                // If not crossover like normal
                else
                {
                    // Generate chance
                    chance = GenerateRandomNumber(1, 100);

                    // Pick which parent to take gene from
                    if (chance > 50)
                    {
                        tempDelayedTime = firstParent.GetDelayedtime();
                    }
                    else
                    {
                        tempDelayedTime = secondParent.GetDelayedtime();
                    }
                }
            }
            // Pick which parent to take gene from
            if (i == 3)
            {
                // Check if mutates
                if (CheckForMutation())
                {
                    // If mutates
                    tempForwardRayLength = GenerateRandomNumber(1, 100);
                }
                // If not crossover like normal
                else
                {
                    // Generate chance
                    chance = GenerateRandomNumber(3, 10);

                    // Pick which parent to take gene from
                    if (chance > 50)
                    {
                        tempForwardRayLength = firstParent.GetForwardRaycastLength();
                    }
                    else
                    {
                        tempForwardRayLength = secondParent.GetForwardRaycastLength();
                    }
                }
            }
            // Pick which parent to take gene from
            if (i == 4)
            {
                // Check if mutates
                if (CheckForMutation())
                {
                    // If mutates
                    tempUpRightRayLength = GenerateRandomNumber(3, 10);
                }
                // If not crossover like normal
                else
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
                }
            }
            // Pick which parent to take gene from
            if (i == 5)
            {
                // Check if mutates
                if (CheckForMutation())
                {
                    // If mutates
                    tempDownRightRayLength = GenerateRandomNumber(3, 10);
                }
                // If not crossover like normal
                else
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
                }
            }
        }

        GenomeDataClass newChild = new GenomeDataClass(tempDelayedThreshold, tempLongThreshold, tempDelayedTime ,tempForwardRayLength, tempUpRightRayLength, tempDownRightRayLength);

        
        nextGenerationGenomeArray.Add(newChild);
    }

    public bool CheckForMutation()
    {
        int chance = Mathf.RoundToInt(Random.Range(1, 100));

        // See if mutation occurs
        if (chance <= mutationChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void AssignData()
    {
        // If in the first generation
        if (generation == 0)
        {
            return;
        }
        // If
        else
        {
            // Itterate through map list
            for (int i = 0; i < mapList.Count; i++)
            {
                mapList[i].GetComponent<LevelScript>().LoadMarioGenome(nextGenerationGenomeArray[i]);
            }
        }
    }

    public int GenerateRandomNumber(int startNumber, int endNumber)
    {
        int randomVariable = Mathf.RoundToInt(Random.Range(startNumber, endNumber));

        return randomVariable;
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

                newMap.GetComponent<LevelScript>().SpawnMario();
            }
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
            xmlGenomeWriter.WriteAttributeString("Generation ",  i.ToString());

            // Create delayed threshold element
            xmlGenomeWriter.WriteStartElement("DelayedThreshold");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetDelayedThreshold().ToString());

            // End the delayed threshold element
            xmlGenomeWriter.WriteEndElement();

            // Create long jump threshold element
            xmlGenomeWriter.WriteStartElement("LongJumpThreshold");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetLongJumpThreshold().ToString());

            // End the long jump threshold element
            xmlGenomeWriter.WriteEndElement();

            // Create delayed time element
            xmlGenomeWriter.WriteStartElement("DelayedTime");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].GetDelayedtime().ToString());

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
