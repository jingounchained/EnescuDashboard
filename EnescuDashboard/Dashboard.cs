using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnescuDashboard
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            try
            {
                Init();
            }catch(Exception ex)
            {
                string s = ex.Message;
            }
        }

        #region Initialization
        private void Init()
        {
            LoadAllTabs();
            this.MdiParent = Genie.Instance.ParentForm;
        }

        private void LoadAllTabs()
        {
            LoadGeneral();
            LoadWeapons();
            LoadMagic();
            LoadGuild();
        }

        private void LoadWeapons()
        {
            foreach (string skill in new string[] { "Bow","Crossbow","Slings",
                                                    "Small_Edged","Large_Edged","Twohanded_Edged",
                                                    "Small_Blunt","Large_Blunt","Twohanded_Blunt",
                                                    "Polearms","Staves"})
            {
                string noun = Genie.Instance.GetVariable("Enescu.Weapons." + skill + ".Noun");
                bool enabled = Genie.Instance.GetVariable("Enescu.Weapons." + skill + ".Enabled") == "1";
                bool swap = Genie.Instance.GetVariable("Enescu.Weapons." + skill + ".Swap") == "1";
                string swapCommand = Genie.Instance.GetVariable("Enescu.Weapons." + skill + ".Swap.Command");
                bool worn = Genie.Instance.GetVariable("Enescu.Weapons." + skill + ".Worn") == "1";
                Weapons.Rows.Add(new object[] { skill, enabled, noun, worn, swap, swapCommand });
            }
            BowAmmo.Text = GetVar("Enescu.Weapons.Bow.Ammo.Noun");
            XBowAmmo.Text = GetVar("Enescu.Weapons.Crossbow.Ammo.Noun");
            SlingAmmo.Text = GetVar("Enescu.Weapons.Slings.Ammo.Noun");
            LightThrown.Text = GetVar("Enescu.Weapons.Light_Thrown.Noun");
            HeavyThrown.Text = GetVar("Enescu.Weapons.Heavy_Thrown.Noun");
            LightThrownEnabled.Checked = GetVar("Enescu.Weapons.Light_Thrown.Enabled") == "1";
            HeavyThrownEnabled.Checked = GetVar("Enescu.Weapons.Heavy_Thrown.Enabled") == "1";
            Offhand.Checked = GetVar("Enescu.Weapons.Offhand.Enabled") == "1";
            Brawling.Checked = GetVar("Enescu.Weapons.Brawling.Enabled") == "1";
        }

        private void LoadGeneral()
        {
            Instrument.Text = GetVar("Enescu.Performance.Instrument");
            Song.Text = GetVar("Enescu.Performance.Song");
            Mood.Text = GetVar("Enescu.Performance.Mood");
            Compendium.Checked = GetVar("Enescu.FirstAid.Compendium.Enabled") == "1";
            Textbook.Checked = GetVar("Enescu.FirstAid.Compendium.Unlocked") == "1";
            CompendiumNoun.Text = GetVar("Enescu.FirstAid.Compendium.Noun");
            ClimbingRope.Checked = GetVar("Enescu.Athletics.ClimbingRope.Enabled") == "1";
            ClimbingRopeNoun.Text = GetVar("Enescu.Athletics.ClimbingRope.Noun");
            Collect.Text = GetVar("Enescu.Outdoorsmanship.Collect.Noun");
            CollectItems.Checked = GetVar("Enescu.Outdoorsmanship.Collect.Enabled") == "1";
            ArrangeAll.Checked = GetVar("Enescu.Skinning.ArrangeAll.Enabled") == "1";
            Bundle.Checked = GetVar("Enescu.Skinning.Bundle.Enabled") == "1";
            AppraisalFocus.Text = GetVar("Enescu.Appraisal.Focus");
            AppraisalItem.Text = GetVar("Enescu.Appraisal.Pouch");
            LockpickTrainer.Checked = GetVar("Enescu.Locksmithing.Trainer.Enabled") == "1";
            LockpickTrainerNoun.Text = GetVar("Enescu.Locksmithing.Trainer.Noun");

            Armor.Items.AddRange(GetVar("Enescu.Armor").Split('|'));
            LoadBuffs();
        }

        private void LoadMagic()
        {
            decimal parsed = 0;

            AugmentationSpell.Text = GetVar("Enescu.Magic.Augmentation.Spell");
            AugmentationAmount.Text = GetVar("Enescu.Magic.Augmentation.Amount");
            AugmentationChargeCount.Text = GetVar("Enescu.Magic.Augmentation.Charge.Count");
            AugmentationChargeAmount.Text = GetVar("Enescu.Magic.Augmentation.Charge.Amount");
            AugmentationRelease.Text = GetVar("Enescu.Magic.Augmentation.Release");
            AugmentationTarget.Text = GetVar("Enescu.Magic.Augmentation.Target");
            AugmentationSymbiosis.Checked = GetVar("Enescu.Magic.Augmentation.Symbiosis") == "1";

            UtilitySpell.Text = GetVar("Enescu.Magic.Utility.Spell");
            UtilityAmount.Text = GetVar("Enescu.Magic.Utility.Amount");
            UtilityChargeCount.Text = GetVar("Enescu.Magic.Utility.Charge.Count");
            UtilityChargeAmount.Text = GetVar("Enescu.Magic.Utility.Charge.Amount");
            UtilityRelease.Text = GetVar("Enescu.Magic.Utility.Release");
            UtilityTarget.Text = GetVar("Enescu.Magic.Utility.Target");
            UtilitySymbiosis.Checked = GetVar("Enescu.Magic.Utility.Symbiosis") == "1";

            WardingSpell.Text = GetVar("Enescu.Magic.Warding.Spell");
            WardingAmount.Text = GetVar("Enescu.Magic.Warding.Amount");
            WardingChargeCount.Text = GetVar("Enescu.Magic.Warding.Charge.Count");
            WardingChargeAmount.Text = GetVar("Enescu.Magic.Warding.Charge.Amount");
            WardingRelease.Text = GetVar("Enescu.Magic.Warding.Release");
            WardingTarget.Text = GetVar("Enescu.Magic.Warding.Target");
            WardingSymbiosis.Checked = GetVar("Enescu.Magic.Warding.Symbiosis") == "1";

            SorcerySpell.Text = GetVar("Enescu.Magic.Sorcery.Spell");
            SorceryAmount.Text = GetVar("Enescu.Magic.Sorcery.Amount");
            SorceryChargeCount.Text = GetVar("Enescu.Magic.Sorcery.Charge.Count");
            SorceryChargeAmount.Text = GetVar("Enescu.Magic.Sorcery.Charge.Amount");
            SorceryRelease.Text = GetVar("Enescu.Magic.Sorcery.Release");
            SorceryTarget.Text = GetVar("Enescu.Magic.Sorcery.Target");
            SorcerySymbiosis.Checked = GetVar("Enescu.Magic.Sorcery.Symbiosis") == "1";

            TargetedSpell.Text = GetVar("Enescu.Magic.Targeted.Spell");
            TargetedAmount.Text = GetVar("Enescu.Magic.Targeted.Amount");
            TargetedChargeCount.Text = GetVar("Enescu.Magic.Targeted.Charge.Count");
            TargetedChargeAmount.Text = GetVar("Enescu.Magic.Targeted.Charge.Amount");
            TargetedSymbiosis.Checked = GetVar("Enescu.Magic.Targeted.Symbiosis") == "1";

            DebilitationSpell.Text = GetVar("Enescu.Magic.Debilitation.Spell");
            DebilitationAmount.Text = GetVar("Enescu.Magic.Debilitation.Amount");
            DebilitationChargeCount.Text = GetVar("Enescu.Magic.Debilitation.Charge.Count");
            DebilitationChargeAmount.Text = GetVar("Enescu.Magic.Debilitation.Charge.Amount");
            DebilitationSymbiosis.Checked = GetVar("Enescu.Magic.Debilitation.Symbiosis") == "1";

            CambrinthNoun.Text = GetVar("Enescu.Magic.Cambrinth.Noun");
            CambrinthWorn.Checked = GetVar("Enescu.Magic.Cambrinth.Worn") == "1";
            CambrinthHeld.Checked = GetVar("Enescu.Magic.Cambrinth.Heldgood omens") == "1";
        }

        private void LoadGuild()
        {
            decimal parsed = 0;

            TelescopeContainer.Text = GetVar("Enescu.Guilds.MoonMage.Telescope.Container");
            PGAmount.Text = GetVar("Enescu.Guilds.MoonMage.PiercingGaze.Amount");

            ThanatologyRitual.Text = GetVar("Enescu.Guilds.Necromancer.Ritual");
            DevourAmount.Text = GetVar("Enescu.Guilds.Necromancer.Devour.Amount");
        }

        private void LoadBuffs()
        {
            int i = 0;
            while (!string.IsNullOrWhiteSpace(GetVar("Enescu.Buffs." + i + ".Spell")))
            {
                string spell = GetVar("Enescu.Buffs." + i + ".Spell");
                string prep = GetVar("Enescu.Buffs." + i + ".Amount");
                string chargeCount = GetVar("Enescu.Buffs." + i + ".Charge.Count");
                string chargeAmount = GetVar("Enescu.Buffs." + i + ".Charge.Amount");
                string spellTimerName = GetVar("Enescu.Buffs." + i + ".SpellTimerName");
                Buffs.Rows.Add(new object[] { spell, prep, chargeCount, chargeAmount, spellTimerName });
                i++;
            }
        }

        #endregion

        #region Functionality

        private void SetVar(string name, string val)
        {
            Genie.Instance.SetVariable(name, val);
        }

        private void SetVar(string name, TextBox varInput)
        {
            SetVar(name, varInput.Text);
        }

        private void SetVar(string name, ComboBox varInput)
        {
            SetVar(name, varInput.Text);
        }

        private void SetVar(string name, CheckBox varInput)
        {
            string var = varInput.Checked ? "1" : "0";
            SetVar(name, var);
        }

        private string GetVar(string name)
        {
            string var = Genie.Instance.GetVariable(name);
            return var;
        }

        private void SaveArmor()
        {
            string armorVariable = "";
            foreach(string item in Armor.Items)
            {
                if (armorVariable.Length > 0) armorVariable += "|";
                armorVariable += item;
            }
            SetVar("Enescu.Armor", armorVariable);
        }

        private void SaveGuild()
        {
            SetVar("Enescu.Guilds.MoonMage.Telescope.Container", TelescopeContainer);
            SetVar("Enescu.Guilds.MoonMage.PiercingGaze.Amount", PGAmount);
            SetVar("Enescu.Guilds.Necromancer.Ritual", ThanatologyRitual);
            SetVar("Enescu.Guilds.Necromancer.Devour.Amount", DevourAmount);
        }

        private void SaveMagic()
        {
            SetVar("Enescu.Magic.Augmentation.Spell", AugmentationSpell);
            SetVar("Enescu.Magic.Augmentation.Target", AugmentationTarget);
            SetVar("Enescu.Magic.Utility.Spell", UtilitySpell);
            SetVar("Enescu.Magic.Utility.Target", UtilityTarget);
            SetVar("Enescu.Magic.Warding.Spell", WardingSpell);
            SetVar("Enescu.Magic.Warding.Target", WardingTarget);
            SetVar("Enescu.Magic.Sorcery.Spell", SorcerySpell);
            SetVar("Enescu.Magic.Sorcery.Target", SorceryTarget);
            SetVar("Enescu.Magic.Targeted.Spell", TargetedSpell);
            SetVar("Enescu.Magic.Debilitation.Spell", DebilitationSpell);
            SetVar("Enescu.Magic.Cambrinth.Noun", CambrinthNoun);
            SetVar("Enescu.Magic.Augmentation.Amount", AugmentationAmount);
            SetVar("Enescu.Magic.Augmentation.Charge.Count", AugmentationChargeCount);
            SetVar("Enescu.Magic.Augmentation.Charge.Amount", AugmentationChargeAmount);
            SetVar("Enescu.Magic.Utility.Amount", UtilityAmount);
            SetVar("Enescu.Magic.Utility.Charge.Count", UtilityChargeCount);
            SetVar("Enescu.Magic.Utility.Charge.Amount", UtilityChargeAmount);
            SetVar("Enescu.Magic.Warding.Amount", WardingAmount);
            SetVar("Enescu.Magic.Warding.Charge.Count", WardingChargeCount);
            SetVar("Enescu.Magic.Warding.Charge.Amount", WardingChargeAmount);
            SetVar("Enescu.Magic.Sorcery.Amount", SorceryAmount);
            SetVar("Enescu.Magic.Sorcery.Charge.Count", SorceryChargeCount);
            SetVar("Enescu.Magic.Sorcery.Charge.Amount", SorceryChargeAmount);
            SetVar("Enescu.Magic.Targeted.Amount", TargetedAmount);
            SetVar("Enescu.Magic.Targeted.Charge.Count", TargetedChargeCount);
            SetVar("Enescu.Magic.Targeted.Charge.Amount", TargetedChargeAmount);
            SetVar("Enescu.Magic.Debilitation.Amount", DebilitationAmount);
            SetVar("Enescu.Magic.Debilitation.Charge.Count", DebilitationChargeCount);
            SetVar("Enescu.Magic.Debilitation.Charge.Amount", DebilitationChargeAmount);
            SetVar("Enescu.Magic.Augmentation.Release", AugmentationRelease);
            SetVar("Enescu.Magic.Utility.Release", UtilityRelease);
            SetVar("Enescu.Magic.Warding.Release", WardingRelease);
            SetVar("Enescu.Magic.Sorcery.Release", SorceryRelease);
            SetVar("Enescu.Magic.Augmentation.Symbiosis", AugmentationSymbiosis);
            SetVar("Enescu.Magic.Utility.Symbiosis", UtilitySymbiosis);
            SetVar("Enescu.Magic.Warding.Symbiosis", WardingSymbiosis);
            SetVar("Enescu.Magic.Sorcery.Symbiosis", SorcerySymbiosis);
            SetVar("Enescu.Magic.Targeted.Symbiosis", TargetedSymbiosis);
            SetVar("Enescu.Magic.Debilitation.Symbiosis", DebilitationSymbiosis);
            SetVar("Enescu.Magic.Cambrinth.Worn", CambrinthWorn);
            SetVar("Enescu.Magic.Cambrinth.Held", CambrinthHeld);
        }

        private void SaveWeapons()
        {
            SetVar("Enescu.Weapons.Offhand.Enabled", Offhand);
            SetVar("Enescu.Weapons.Brawling.Enabled", Brawling);
            SetVar("Enescu.Weapons.Heavy_Thrown.Enabled", HeavyThrownEnabled);
            SetVar("Enescu.Weapons.Light_Thrown.Enabled", LightThrownEnabled);
            SetVar("Enescu.Weapons.Bow.Ammo.Noun", BowAmmo);
            SetVar("Enescu.Weapons.Crossbow.Ammo.Noun", XBowAmmo);
            SetVar("Enescu.Weapons.Bow.Slings.Noun", SlingAmmo);
            SetVar("Enescu.Weapons.Light_Thrown.Noun", LightThrown);
            SetVar("Enescu.Weapons.Heavy_Thrown.Noun", HeavyThrown);

            for (int rowIndex = 0; rowIndex < Weapons.Rows.Count; rowIndex++)
            {
                for (int colIndex = 1; colIndex < Weapons.Columns.Count; colIndex++)
                {
                    string varName = "Enescu.Weapons." + Weapons[colIndex, rowIndex].Value.ToString() + "." +
                            (Weapons.Columns[colIndex].Name == "Command" ? "Swap.Command" :
                             Weapons.Columns[colIndex].Name);
                    SetVar(varName, Weapons[colIndex, rowIndex].Value.ToString());
                }

            }
            
        }

        private void SaveGeneral()
        {
            SetVar("Enescu.Performance.Mood", Mood);
            SetVar("Enescu.Performance.Song", Song);
            SetVar("Enescu.Performance.Instrument", Instrument);
            SetVar("Enescu.Appraisal.Pouch", AppraisalItem);
            SetVar("Enescu.Appraisal.Focus", AppraisalFocus);
            SetVar("Enescu.Outdoorsmanship.Collect.Noun", Collect);
            SetVar("Enescu.Locksmithing.Trainer.Noun", LockpickTrainerNoun);
            SetVar("Enescu.Athletics.ClimbingRope.Noun", ClimbingRopeNoun);
            SetVar("Enescu.FirstAid.Compendium.Noun", CompendiumNoun);
            SetVar("Enescu.FirstAid.Compendium.Unlocked", Textbook);
            SetVar("Enescu.Skinning.Bundle.Enabled", Bundle);
            SetVar("Enescu.Skinning.ArrangeAll.Enabled", ArrangeAll);
            SetVar("Enescu.Outdoorsmanship.Collect.Enabled", CollectItems);
            SetVar("Enescu.Locksmithing.Trainer.Enabled", LockpickTrainer);
            SetVar("Enescu.FirstAid.Compendium.Enabled", Compendium);
            SetVar("Enescu.Athletics.ClimbingRope.Enabled", ClimbingRope);
            SaveBuffs();
            SaveArmor();
        }

        private void SaveBuffs()
        {
            for (int rowIndex = 0; rowIndex < Buffs.Rows.Count - 1; rowIndex++)
            {
                for (int colIndex = 0; colIndex < Buffs.Columns.Count - 1; colIndex++)
                {
                    string varName = "Enescu.Buffs." + rowIndex + "." + Buffs.Columns[colIndex].Name;
                    SetVar(varName, Buffs[colIndex, rowIndex].Value.ToString());
                }
            }
        }

        private void SaveAll()
        {
            SaveGeneral();
            SaveWeapons();
            SaveMagic();
            SaveGuild();
            Genie.Instance.Echo("All Enescu Variables Saved.");
            Genie.Instance.SendText("#save variables");
        }

        #endregion

        //#region EventHandlers
        private void Compendium_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.FirstAid.Compendium.Enabled", Compendium);
        }

        private void ClimbingRope_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Athletics.ClimbingRope.Enabled", ClimbingRope);
        }

        private void LockpickTrainer_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Locksmithing.Trainer.Enabled", LockpickTrainer);
        }

        private void CollectItems_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Outdoorsmanship.Collect.Enabled", CollectItems);
        }

        private void ArrangeAll_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Skinning.ArrangeAll.Enabled", ArrangeAll);
        }

        private void Bundle_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Skinning.Bundle.Enabled", Bundle);
        }

        private void Textbook_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.FirstAid.Compendium.Unlocked", Textbook);
        }

        private void CompendiumNoun_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.FirstAid.Compendium.Noun", CompendiumNoun);
        }

        private void ClimbingRopeNoun_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Athletics.ClimbingRope.Noun", ClimbingRopeNoun);
        }

        private void LockpickTrainerNoun_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Locksmithing.Trainer.Noun", LockpickTrainerNoun);
        }

        private void Collect_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Outdoorsmanship.Collect.Noun", Collect);
        }

        private void AppraisalFocus_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Appraisal.Focus", AppraisalFocus);
        }

        private void AppraisalItem_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Appraisal.Pouch", AppraisalItem);
        }

        private void Instrument_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Performance.Instrument", Instrument);
        }

        private void Song_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Performance.Song", Song);
        }

        private void Mood_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Performance.Mood", Mood);
        }

        private void Buffs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            string varName = "Enescu.Buffs." + e.RowIndex + "." + Buffs.Columns[e.ColumnIndex].Name;
            SetVar(varName, Buffs[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        private void BuffDelete_Click(object sender, EventArgs e)
        {

            foreach(DataGridViewColumn col in Buffs.Columns)
            {
                SetVar("Enescu.Buffs." + (Buffs.Rows.Count - 1).ToString() + "." + col.Name,"");
            }
            Buffs.Rows.RemoveAt(Buffs.CurrentRow.Index);
            for(int i = 0;i < Buffs.RowCount - 1; i++)
            {
                foreach (DataGridViewColumn col in Buffs.Columns)
                {
                    SetVar("Enescu.Buffs." + i.ToString() + "." + col.Name, Buffs[col.Index,i].Value.ToString());
                }
            }
            SetVar("Enescu.Buffs.Count", Buffs.RowCount.ToString());
        }

        private void ArmorRemove_Click(object sender, EventArgs e)
        {
            Armor.Items.RemoveAt(Armor.SelectedIndex);
            SaveArmor();
        }

        private void ArmorAdd_Click(object sender, EventArgs e)
        {
            Armor.Items.Add(ArmorInput.Text);
            ArmorInput.Text = "";
            SaveArmor();
        }

        private void Weapons_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            string varName = "Enescu.Weapons." + Weapons[0, e.RowIndex].Value.ToString() + "." +
                (Weapons.Columns[e.ColumnIndex].Name == "Command" ? "Swap.Command" :
                Weapons.Columns[e.ColumnIndex].Name);
            SetVar(varName, Weapons[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        private void Offhand_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Weapons.Offhand.Enabled", Offhand);
        }

        private void Brawling_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Weapons.Brawling.Enabled", Brawling);
        }

        private void BowAmmo_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Weapons.Bow.Ammo.Noun", BowAmmo);
        }

        private void XBowAmmo_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Weapons.Crossbow.Ammo.Noun", XBowAmmo);
        }

        private void SlingAmmo_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Weapons.Bow.Slings.Noun", SlingAmmo);
        }

        private void HeavyThrownEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Weapons.Heavy_Thrown.Enabled", HeavyThrownEnabled);
        }

        private void LightThrownEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Weapons.Light_Thrown.Enabled", LightThrownEnabled);
        }

        private void LightThrown_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Weapons.Light_Thrown.Noun", LightThrown);
        }

        private void HeavyThrown_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Weapons.Heavy_Thrown.Noun", HeavyThrown);
        }

        private void AugmentationSpell_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Augmentation.Spell", AugmentationSpell);
        }

        private void AugmentationTarget_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Augmentation.Target", AugmentationTarget);
        }

        private void UtilitySpell_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Utility.Spell", UtilitySpell);
        }

        private void UtilityTarget_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Utility.Target", UtilityTarget);
        }

        private void WardingSpell_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Warding.Spell", WardingSpell);
        }

        private void WardingTarget_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Warding.Target", WardingTarget);
        }

        private void SorcerySpell_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Sorcery.Spell", SorcerySpell);
        }

        private void SorceryTarget_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Sorcery.Target", SorceryTarget);
        }

        private void TargetedSpell_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Targeted.Spell", TargetedSpell);
        }

        private void DebilitationSpell_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Debilitation.Spell", DebilitationSpell);
        }

        private void CambrinthNoun_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Cambrinth.Noun", CambrinthNoun);
        }

        private void AugmentationAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Augmentation.Amount", AugmentationAmount);
        }

        private void AugmentationChargeCount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Augmentation.Charge.Count", AugmentationChargeCount);
        }

        private void AugmentationChargeAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Augmentation.Charge.Amount", AugmentationChargeAmount);
        }

        private void UtilityAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Utility.Amount", UtilityAmount);
        }

        private void UtilityChargeCount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Utility.Charge.Count", UtilityChargeCount);
        }

        private void UtilityChargeAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Utility.Charge.Amount", UtilityChargeAmount);
        }

        private void WardingAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Warding.Amount", WardingAmount);
        }

        private void WardingChargeCount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Warding.Charge.Count", WardingChargeCount);
        }

        private void WardingChargeAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Warding.Charge.Amount", WardingChargeAmount);
        }

        private void SorceryAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Sorcery.Amount", SorceryAmount);
        }

        private void SorceryChargeCount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Sorcery.Charge.Count", SorceryChargeCount);
        }

        private void SorceryChargeAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Sorcery.Charge.Amount", SorceryChargeAmount);
        }

        private void TargetedAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Targeted.Amount", TargetedAmount);
        }

        private void TargetedChargeCount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Targeted.Charge.Count", TargetedChargeCount);
        }

        private void TargetedChargeAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Targeted.Charge.Amount", TargetedChargeAmount);
        }

        private void DebilitationAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Debilitation.Amount", DebilitationAmount);
        }

        private void DebilitationChargeCount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Debilitation.Charge.Count", DebilitationChargeCount);
        }

        private void DebilitationChargeAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Debilitation.Charge.Amount", DebilitationChargeAmount);
        }

        private void AugmentationRelease_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Augmentation.Release", AugmentationRelease.SelectedText);
        }

        private void UtilityRelease_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Utility.Release", UtilityRelease.SelectedText);
        }

        private void WardingRelease_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Warding.Release", WardingRelease.SelectedText);
        }

        private void SorceryRelease_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Sorcery.Release", SorceryRelease.SelectedText);
        }

        private void AugmentationSymbiosis_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Augmentation.Symbiosis", AugmentationSymbiosis);
        }

        private void UtilitySymbiosis_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Utility.Symbiosis", UtilitySymbiosis);
        }

        private void WardingSymbiosis_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Warding.Symbiosis", WardingSymbiosis);
        }

        private void SorcerySymbiosis_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Sorcery.Symbiosis", SorcerySymbiosis);
        }

        private void TargetedSymbiosis_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Targeted.Symbiosis", TargetedSymbiosis);
        }

        private void DebilitationSymbiosis_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Debilitation.Symbiosis", DebilitationSymbiosis);
        }

        private void CambrinthWorn_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Cambrinth.Worn", CambrinthWorn);
        }

        private void CambrinthHeld_CheckedChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Magic.Cambrinth.Held", CambrinthHeld);
        }

        private void TelescopeContainer_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Guilds.MoonMage.Telescope.Container", TelescopeContainer);
        }

        private void PGAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Guilds.MoonMage.PiercingGaze.Amount", PGAmount);
        }

        private void ThanatologyRitual_TextChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Guilds.Necromancer.Ritual", ThanatologyRitual);
        }

        private void DevourAmount_ValueChanged(object sender, EventArgs e)
        {
            SetVar("Enescu.Guilds.Necromancer.Devour.Amount", DevourAmount);
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAll();
        }
        
        private void Save_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        private void Dock_Click(object sender, EventArgs e)
        {
            if(this.MdiParent == null)
            {
                this.MdiParent = Genie.Instance.ParentForm;
                Dock.Text = "Undock";
            }else
            {
                this.MdiParent = null;
                Dock.Text = "Dock";
            }
        }
    }
}
