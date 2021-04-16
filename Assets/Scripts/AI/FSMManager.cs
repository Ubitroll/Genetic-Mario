using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class FSMManager : MonoBehaviour
{
    // Variables
    GameObject[] marioArray;

    public GenomeDataClass[] currentMarioGenomeArray;
    public GenomeDataClass[] bestSeedMarioGenomeArray;

    // File String
    public string genomeFileName = "GenomeFile";

    // Start is called before the first frame update
    void Start()
    {
        
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
        for (int i =0; i < bestSeedMarioGenomeArray.GetLength(0); i++)
        {
            // Create a single Genome Element
            xmlGenomeWriter.WriteStartElement("Genome");

            // Add the attribute of which generation is being written
            xmlGenomeWriter.WriteAttributeString("Generation ",  i.ToString());

            // Create delayed threshold element
            xmlGenomeWriter.WriteStartElement("DelayedThreshold");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].getDelayedThreshold().ToString());

            // End the delayed threshold element
            xmlGenomeWriter.WriteEndElement();

            // Create long jump threshold element
            xmlGenomeWriter.WriteStartElement("LongJumpThreshold");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].getLongJumpThreshold().ToString());

            // End the long jump threshold element
            xmlGenomeWriter.WriteEndElement();

            // Create delayed time element
            xmlGenomeWriter.WriteStartElement("DelayedTime");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].getDelayedtime().ToString());

            // End the delayed time element
            xmlGenomeWriter.WriteEndElement();

            // Create forward raycast length element
            xmlGenomeWriter.WriteStartElement("ForwardRaycastLength");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].getForwardRaycastLength().ToString());

            // End the forward raycast length element
            xmlGenomeWriter.WriteEndElement();

            // Create forward up raycast length element
            xmlGenomeWriter.WriteStartElement("ForwardUpRaycastLength");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].getForwardUpRaycastLength().ToString());

            // End the forward up raycast length element
            xmlGenomeWriter.WriteEndElement();

            // Create forward down raycast length element
            xmlGenomeWriter.WriteStartElement("ForwardDownRaycastLength");

            // Write in genome values
            xmlGenomeWriter.WriteString(bestSeedMarioGenomeArray[i].getForwardDownRaycastLength().ToString());

            // End the forward down raycast length element
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
