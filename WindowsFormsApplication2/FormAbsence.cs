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
    public partial class FormAbsence : Form
    {
        public FormAbsence()
        {
            InitializeComponent();
        }

        //Setter de la class FormAbsence pour passer l'info du formulaire Employé à ce formualire
        public void SetTextBox4(string param1)
        {
            this.textBox4.Text = param1;
        }
        private void FormAbsence_Load(object sender, EventArgs e)
        {
            //Début code
            if (this.Text == "Ajout Absence(s)") this.button1.Text = "&Ajout";
            else
            {
                this.button1.Text = "&Modifier";

                if (String.IsNullOrEmpty(textBox4.Text))
                {
                    MessageBox.Show("Saisir une Absence à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
                }
                else
                {
                    Absence d = new Absence(); //Class Data avec méthodes car class importer sur même NameSpace                    
                    d.SetId_absence(textBox4.Text);
                    SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                    DataTable table = new DataTable();  //DataSet
                    sda.Fill(table);
                    textBox1.Text = table.Rows[0][0].ToString(); //Alimenter le champs id avec la valeur passé en paramètre
                    textBox2.Text = table.Rows[0][1].ToString(); //Alimenter le champs Nom avec resultat select sur id passé en paramètre
                    textBox3.Text = table.Rows[0][2].ToString(); //Alimenter le champs Prénom avec resultat select sur id passé en paramètre
                    textBox4.Text = table.Rows[0][3].ToString(); //Alimenter le champs Adresse avec resultat select sur id passé en paramètre
                    textBox5.Text = table.Rows[0][4].ToString(); //Alimenter le champs Téléphone avec resultat select sur id passé en paramètre                    
                    d.deconnecter(); //Fin connexion   
                }//Fin Si2
            } //Fin Si1
            //Fin code
        }

        //Ajouter - Modifier Absence
        private void button1_Click(object sender, EventArgs e)
        {
            //Début code commun Ajout & Modifier            
            Absence d = new Absence(); //Class Data avec méthodes car class importer sur même NameSpace
            d.SetId_employe(textBox1.Text); //nnchar(15) id employe
            d.SetDate_debut(DateTime.Parse(textBox2.Text)); //datetime
            d.SetDate_fin(DateTime.Parse(textBox3.Text)); //datetime
            d.SetId_absence(textBox4.Text); //nchar(50) id absence
            d.SetMotif(textBox5.Text.Trim()); //nchar(10)         motif
            
            //Si champ heure_debut ou heure_fin sontvides que cela soit en modif ou ajout, on affiche un message sinon on enregistre
            if (textBox6.Text.Length == 0)
            {
                MessageBox.Show("champs heure début non renseignéé !!! ", "Infos Saisie", MessageBoxButtons.OK);
            }
            else
            {
                d.SetHeure_debut(DateTime.Parse(textBox6.Text)); //time
            }

            if (textBox7.Text.Length == 0)
            {
                MessageBox.Show("champs heure fin non renseignéé !!! ", "Infos Saisie", MessageBoxButtons.OK);
            }
            else
            {
                d.SetHeure_fin(DateTime.Parse(textBox7.Text)); //time         
            }

            //si champ heure_debut ou heure_fin sontvides que cela soit en modif ou ajout, on affiche un message sinon on enregistre
            if (textBox6.Text.Length != 0 || textBox7.Text.Length != 0)
            {              
                if (this.button1.Text == "&Ajout")
                {
                    SqlDataAdapter sda = d.AfficherTous(d.Requete("Ajouter"));
                }
                else
                {
                    SqlDataAdapter sda = d.AfficherTous(d.Requete("Modifier"));
                }//Finsi1
            }//Fin si0
            d.deconnecter(); //Fin connexion
            //MessageBox.Show("Enregistrement crée !!! ", "Saisie OK", MessageBoxButtons.OK);           
            //Fin code
        }
    }
}
