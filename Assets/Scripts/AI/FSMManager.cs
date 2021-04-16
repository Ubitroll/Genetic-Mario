using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FSMManager : MonoBehaviour
{
    // Variables
    
    // Lists
    List<GameObject> mapList;



    public List<GenomeDataClass> currentGenerationGenomeArray;
    public List<GenomeDataClass> bestSeedMarioGenomeArray;

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

    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
