using Base_Dynamic_Jet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class FormClient2 : Form
    {
        public FormClient2()
        {
            InitializeComponent();
        }

        //Setter pour passage info du formulaire Client à ce formulaire
        public void SetTextBox1(string param1){
            this.textBox1.Text = param1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {   
            //Début code
            if (this.Text != "Vérification doublon Client(s)") { 
            if (this.Text == "Ajout Client(s)") this.button1.Text = "&Ajout";
            else
            {
                this.button1.Text = "&Modifier";                

                if (String.IsNullOrEmpty(textBox1.Text) )
                {
                    MessageBox.Show("Saisir un identifant Client à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
                }
                else
                {
                    Client d = new Client(); //Class Data avec méthodes car class importer sur même NameSpace
                    d.SetId_client(Int32.Parse(textBox1.Text));
                    SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                    DataTable table = new DataTable();  //DataSet
                    sda.Fill(table);
                    //textBox1.Text = table.Rows[0][0].ToString(); //Alimenter le champs id avec la valeur passé en paramètre
                    textBox2.Text = table.Rows[0][1].ToString(); //Alimenter le champs Nom avec resultat select sur id passé en paramètre
                    textBox3.Text = table.Rows[0][2].ToString(); //Alimenter le champs Prénom avec resultat select sur id passé en paramètre
                    textBox4.Text = table.Rows[0][3].ToString(); //Alimenter le champs Adresse avec resultat select sur id passé en paramètre
                    textBox5.Text = table.Rows[0][4].ToString(); //Alimenter le champs Téléphone avec resultat select sur id passé en paramètre
                    textBox6.Text = table.Rows[0][5].ToString(); //Alimenter le champs Date de naissance avec resultat select sur id passé en paramètre
                    textBox7.Text = table.Rows[0][6].ToString(); //Alimenter le champs N° de permis avec resultat select sur id passé en paramètre
                    textBox8.Text = table.Rows[0][7].ToString(); //Alimenter le champs Mail avec resultat select sur id passé en paramètre
                    d.deconnecter(); //Fin connexion   
                }//Fin Si2
            } //Fin Si1
            }//Fin si0
            //Fin code
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Début code commun Ajout & Modifier            
            Client d = new Client(); //Class Data avec méthodes car class importer sur même NameSpace
            d.SetId_client(Int32.Parse(textBox1.Text)); //int idclient
            d.SetNom(textBox2.Text.Trim()); ////nchar(15) nom
            d.SetPrenom(textBox3.Text.Trim()); ////nchar(15) prenom
            d.SetAdresse(textBox4.Text.Trim()); ////nchar(50) adresse
            d.SetTelephone(textBox5.Text.Trim()); ////nchar(20) telephone
            d.SetDate_naissance(DateTime.Parse(textBox6.Text)); ////Date naissance
            d.SetNumero_permis(textBox7.Text.Trim()); ////nchar(50) permis
            d.SetMail(textBox8.Text.Trim()); ////nchar(30) mail           

            if (this.button1.Text == "&Ajout") { 
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Ajouter"));
            }else{
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Modifier"));
            }
            d.deconnecter(); //Fin connexion            
            //Fin code
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Anti doublon: choisir un SEUL critère parmi la liste
        private void button2_Click(object sender, EventArgs e)
        {
            //Début code commun Ajout & Modifier      
            string ChCritères = "id_client >= 1";

            Client d = new Client(); //Class Data avec méthodes car class importer sur même NameSpace
            if (textBox1.Text.Length > 0)
            {
                ChCritères = "id_client = " + textBox1.Text + " ";
            }

            if (textBox2.Text.Length > 0)
            {
                ChCritères =  "nom = '" + textBox2.Text + "' ";
            }

            if (textBox3.Text.Length > 0)
            {
                ChCritères =  "prenom = '" + textBox3.Text + "' ";
            }

            if (textBox5.Text.Length > 0)
            {
                ChCritères =  "telephone = '" + textBox5.Text + "' ";
            }

            if (textBox6.Text.Length > 0)
            {
                ChCritères =  "date_naissance = '" + textBox6.Text + "' ";
            }

            if (textBox7.Text.Length > 0)
            {
                ChCritères =  "numero_permis = '" + textBox7.Text + "' ";
            }

            if (textBox8.Text.Length > 0)
            {
                ChCritères =   "mail = '" + textBox8.Text + "' ";
            }

            /*
            d.SetNom(textBox1.Text); ////int id client
            d.SetNom(textBox2.Text); ////nchar(15) nom
            d.SetPrenom(textBox3.Text); ////nchar(15) prenom
            d.SetAdresse(textBox4.Text); ////nchar(50) adresse
            d.SetTelephone(textBox5.Text); ////nchar(20) telephone
            d.SetDate_naissance(DateTime.Parse(textBox6.Text)); ////Date naissance
            d.SetNumero_permis(textBox7.Text); ////nchar(50) permis
            d.SetMail(textBox8.Text); ////nchar(30) mail           
            */
            d.SetChCritères(ChCritères);
            MessageBox.Show("Chaîne critères= "+ ChCritères, "Infos Doublon", MessageBoxButtons.OK);
            
            SqlDataAdapter sda = d.AfficherTous(d.Requete("Doublon"));
            DataTable table = new DataTable();  //DataSet = mode déconnecté donc pasconnexion base en continue
            sda.Fill(table);

            if (!String.IsNullOrEmpty(table.Rows[0][0].ToString()))
            {
                if (Int32.Parse(table.Rows[0][0].ToString()) >= 1) MessageBox.Show("Ce Client existe !!! ", "Infos Doublon", MessageBoxButtons.OK);
                else MessageBox.Show("C'est bon ce Client n'existe pas encore !!! ", "Infos Doublon", MessageBoxButtons.OK);
            }
            else MessageBox.Show("C'est bon ce Client n'existe pas encore !!! ", "Infos Doublon", MessageBoxButtons.OK);

            d.deconnecter(); //Fin connexion            
            //Fin code
        }
    }
}
