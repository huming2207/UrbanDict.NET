using System;
using System.Xml;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UrbanToApple
{
	public class XmlHandler
	{
		public XmlWriter xmlWriter;

		public XmlHandler(string xmlPath)
		{
			xmlWriter = XmlWriter.Create(xmlPath + "urban_dict.xml");
		}

		/*
			The header format is:
				<?xml version="1.0" encoding="UTF-8"?>
				<d:dictionary xmlns="http://www.w3.org/1999/xhtml" xmlns:d="http://www.apple.com/DTDs/DictionaryService-1.0.rng">
				
			In this function we need to create something like this.
		*/

		public void AddHeader()
		{
			xmlWriter.WriteStartDocumentAsync();
			xmlWriter.WriteStartElement("d:dictionary", "http://www.w3.org/1999/xhtml");
			xmlWriter.WriteAttributeString("xmlns", "d", null, "http://www.apple.com/DTDs/DictionaryService-1.0.rng");
		}


		/*
			Each entry should be like:
			
				<d:entry id="dictionary_application" d:title="Dictionary application">
					<d:index d:value="Dictionary application"/>
					<h1>Dictionary application </h1>
					<p>
						An application to look up dictionary on Mac OS X.<br/>
					</p>
				</d:entry>

		*/

		public void AddEntry(string entryId, string entryTitle, List<string> indexValues, Dictionary<string,string> explanations)
		{
			// For the line: <d:entry id="dictionary_application" d:title="Dictionary application">
			xmlWriter.WriteStartElement("d:entry");
			xmlWriter.WriteAttributeString("id", entryId);
			xmlWriter.WriteAttributeString("d:title", entryTitle);

			// Add search index to the dictionary.
			addIndex(indexValues);

			// For the "Heading 1" line: <h1>value</h1>
			addHeading(entryTitle, 1);


			// Add the explanation list
			addExplanation(explanations);

			// Finalize the entry input, add </d:entry> to it.
			xmlWriter.WriteEndElement();
		}

		private void addExplanation(Dictionary<string, string> explanations)
		{
			foreach (KeyValuePair<string, string> content in explanations)
			{
				// For the explanation:
				addHeading("Explanation: ", 2);
				addParagraph(content.Key);

				// Add some empty lines
				addEmptyLine();

				// For the example:
				addHeading("Example: ", 2);
				addParagraph(content.Value);

				// Add some empty lines
				addEmptyLine();
			}
		}

		public void AddEnding()
		{
			// Add the ending symbol "</d:dictionary>"
			xmlWriter.WriteEndElement();
		}

		public void Flush()
		{
			xmlWriter.Flush();
		}

		public void Dispose()
		{
			xmlWriter.Close();
		}

		private void addIndex(List<string> indexValues)
		{
			foreach (string indexValue in indexValues)
			{
				// For the line: <d:index d:value="value"/>
				// This is the keyword where it will be added into the dictionary.
				xmlWriter.WriteStartElement("d:index");
				xmlWriter.WriteAttributeString("d:vaule", indexValue);
				xmlWriter.WriteEndElement();
			}
		}

		private void addHeading(string content, uint headingLevel = 1)
		{
			// For the line: <h1>value</h1>
			xmlWriter.WriteStartElement("h" + headingLevel.ToString());
			xmlWriter.WriteString(content);
			xmlWriter.WriteEndElement();
		}

		private void addParagraph(string content)
		{
			// For the line: <p></p> (where it holds the content)
			xmlWriter.WriteStartElement("p");
			xmlWriter.WriteString(content);
			xmlWriter.WriteEndElement();
		}

		private void addEmptyLine()
		{
			// For the line: <p></p> 
			xmlWriter.WriteStartElement("p");
			xmlWriter.WriteEndElement();
		}

		private void addBoldContent(string content)
		{
			// For the line: <b></b>
			xmlWriter.WriteStartElement("b");
			xmlWriter.WriteString(content);
			xmlWriter.WriteEndElement();
		}
	}
}