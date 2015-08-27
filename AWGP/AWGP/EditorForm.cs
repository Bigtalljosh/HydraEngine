using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Reflection;
using SharedContent;

namespace AWGP
{
    public partial class EditorForm : Form
    {
        public EditorForm()
        {
            InitializeComponent();
        }

        /*public void LoadXML()             THIS ISN'T NEEDED ANY MORE AS FAR AS I CAN TELL BUT KEEP IT JUST IN CASE
        {
            GameServiceContainer services = new GameServiceContainer();
            ContentManager Content = new ContentManager(services);
            Content.RootDirectory = "Content";

            appConfig = Content.Load<ApplicationConfig>("ApplicationSettings");
        }*/

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SuperAwesomeCoolGuys Hydra Game Engine" + "\n" + "Programmed By:" + "\n" + "\tJosh Dadak" + "\n" +  "\tShaun Taylor" + "\n\nMusic and SFX created on request by: \n\t Jensen Forshaw \nFor a personal project of Josh Dadak \nSounds are used only with permission");
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            String firstNum, lastNum;

            // Gets the first 4 and last 4 numbers from the Selectionbox, and saves them to a variable
            firstNum = resolutionSelect.Text.Substring(0, 4);
            lastNum = resolutionSelect.Text.Substring(Math.Max(0, resolutionSelect.Text.Length - 4));

            // Removes any spaces from the above strings.
            firstNum = firstNum.Replace(" ", "");
            lastNum = lastNum.Replace(" ", "");

            // Loads the Application Settings XML file
            System.Xml.XmlDocument appConfigXML = new System.Xml.XmlDocument();
            System.Xml.XmlDocument serConfigXML = new System.Xml.XmlDocument();
            appConfigXML.Load(Game._path + "\\Content\\ApplicationSettings.xml");
            serConfigXML.Load(Game._path + "\\Content\\ServiceSettings.xml");
            
            // Sets the XML attributes to the current values of the editor
            appConfigXML.SelectSingleNode("//ScreenTitle").InnerText = windowTitleBox.Text;
            appConfigXML.SelectSingleNode("//MouseActive").InnerText = isMouseActive.Text;
            appConfigXML.SelectSingleNode("//FullScreen").InnerText = isFullScreen.Text;
            appConfigXML.SelectSingleNode("//ScreenWidth").InnerText = Convert.ToString(firstNum);
            appConfigXML.SelectSingleNode("//ScreenHeight").InnerText = Convert.ToString(lastNum);

            serConfigXML.SelectSingleNode("//isInputServiceActive").InnerText = isInputActive.Text;
            serConfigXML.SelectSingleNode("//isPhysicsServiceActive").InnerText = isPhysicsActive.Text;
            serConfigXML.SelectSingleNode("//isScreenServiceActive").InnerText = isScreenActive.Text;
            serConfigXML.SelectSingleNode("//isUserServiceActive").InnerText = isUserActive.Text;
            serConfigXML.SelectSingleNode("//isOtherServiceActive").InnerText = isOtherActive.Text;

            // Saves the Application Settings XML file
            appConfigXML.Save(Game._path + "\\Content\\ApplicationSettings.xml");
            serConfigXML.Save(Game._path + "\\Content\\ServiceSettings.xml");

            MessageBox.Show("Reload Application for changes to take effect");
            System.Diagnostics.Process.Start(Game._path + @"\\AWGP.exe");
            Application.Exit();
        }

        private void EditorForm_Load(object sender, EventArgs e)
        {
            // Loads the Application Settings XML file
            System.Xml.XmlDocument appConfigXML = new System.Xml.XmlDocument();
            System.Xml.XmlDocument serConfigXML = new System.Xml.XmlDocument();
            appConfigXML.Load(Game._path + "\\Content\\ApplicationSettings.xml");
            serConfigXML.Load(Game._path + "\\Content\\ServiceSettings.xml");

            // First Tab (Application Settings)
            windowTitleBox.Text = appConfigXML.SelectSingleNode("//ScreenTitle").InnerText;
            resolutionSelect.Text = appConfigXML.SelectSingleNode("//ScreenWidth").InnerText + " x " + appConfigXML.SelectSingleNode("//ScreenHeight").InnerText;
            isMouseActive.Text = Convert.ToString(appConfigXML.SelectSingleNode("//MouseActive").InnerText);
            isFullScreen.Text = Convert.ToString(appConfigXML.SelectSingleNode("//FullScreen").InnerText);

            // Second Tab (Service Settings)
            isInputActive.Text = serConfigXML.SelectSingleNode("//isInputServiceActive").InnerText;
            isPhysicsActive.Text = serConfigXML.SelectSingleNode("//isPhysicsServiceActive").InnerText;
            isScreenActive.Text = serConfigXML.SelectSingleNode("//isScreenServiceActive").InnerText;
            isUserActive.Text = serConfigXML.SelectSingleNode("//isUserServiceActive").InnerText;
            isOtherActive.Text = serConfigXML.SelectSingleNode("//isOtherServiceActive").InnerText;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
