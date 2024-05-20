using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.MedicalRecords
{
    [Table("PCI_REPORT")]
    public partial class PciReport
    {
        [Column("PAT_SSN")]
        [StringLength(14)]
        [Unicode(false)]
        public string? PatSsn { get; set; }

        [Key]
        [Column("SERIAL_HISTORY")]
        public int SerialHistory { get; set; }

        [Column("ENTRY_DATE", TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }

        [Column("FIRST_OPERATOR")]
        [StringLength(20)]
        [Unicode(false)]
        public string? FirstOperator { get; set; }

        [Column("SECOND_OPERATOR")]
        [StringLength(20)]
        [Unicode(false)]
        public string? SecondOperator { get; set; }

        [Column("TYPE_OF_ADMISSION")]
        [StringLength(30)]
        [Unicode(false)]
        public string? TypeOfAdmission { get; set; }

        [Column("REPURTUSION_STRATEGY")]
        [StringLength(30)]
        [Unicode(false)]
        public string? RepurtusionStrategy { get; set; }

        [Column("AMOUNT_OF_CONTRAST_PCI_ML")]
        [StringLength(20)]
        [Unicode(false)]
        public string? AmountOfContrastPciMl { get; set; }

        [Column("PCI_ACCESS")]
        [StringLength(20)]
        [Unicode(false)]
        public string? PciAccess { get; set; }

        [Column("INTRA_AORTIC_BALLOON")]
        public double? IntraAorticBalloon { get; set; }

        [Column("ADENOSINE_LC")]
        public double? AdenosineLc { get; set; }

        [Column("GP_INHIBITOS_LC")]
        public double? GpInhibitosLc { get; set; }

        [Column("ADRENALINE_LC")]
        public double? AdrenalineLc { get; set; }

        [Column("HEPARIN_LC")]
        public double? HeparinLc { get; set; }

        [Column("TRIDIL_LC")]
        public double? TridilLc { get; set; }

        [Column("CCB_LC")]
        public double? CcbLc { get; set; }

        [Column("HEMATOMA_PCI")]
        public double? HematomaPci { get; set; }

        [Column("CIN_PCI")]
        public double? CinPci { get; set; }

        [Column("PERFORATION_PCI")]
        public double? PerforationPci { get; set; }

        [Column("DISECTION_PCI")]
        public double? DisectionPci { get; set; }

        [Column("NO_REFLOW")]
        public double? NoReflow { get; set; }

        [Column("OUTCOME_OF_NO_REFLOW")]
        [StringLength(30)]
        [Unicode(false)]
        public string? OutcomeOfNoReflow { get; set; }

        [Column("METHOD_OF_NO_REFLOW_MANAGEMENT")]
        [StringLength(30)]
        [Unicode(false)]
        public string? MethodOfNoReflowManagement { get; set; }

        [Column("DEATH_PCI")]
        public double? DeathPci { get; set; }

        [Column("STEP_ONE_OF_PCI")]
        [StringLength(500)]
        [Unicode(false)]
        public string? StepOneOfPci { get; set; }

        [Column("STEP_2_PCI")]
        [StringLength(500)]
        [Unicode(false)]
        public string? Step2Pci { get; set; }

        [Column("STEP_3_PCI")]
        [StringLength(500)]
        [Unicode(false)]
        public string? Step3Pci { get; set; }

        [Column("STEP_4_PCI")]
        [StringLength(500)]
        [Unicode(false)]
        public string? Step4Pci { get; set; }

        [Column("STEP_5_PCI")]
        [StringLength(500)]
        [Unicode(false)]
        public string? Step5Pci { get; set; }

        [Column("STEP_6_PCI")]
        [StringLength(500)]
        [Unicode(false)]
        public string? Step6Pci { get; set; }

        [Column("STEP_7_PCI")]
        [StringLength(500)]
        [Unicode(false)]
        public string? Step7Pci { get; set; }

        [Column("STEP_8_PCI")]
        [StringLength(500)]
        [Unicode(false)]
        public string? Step8Pci { get; set; }

        [Column("DIAGNOSIS_OF_PCI")]
        [StringLength(500)]
        [Unicode(false)]
        public string? DiagnosisOfPci { get; set; }

        [Column("RECOMENDATION_PCI")]
        [StringLength(500)]
        [Unicode(false)]
        public string? RecomendationPci { get; set; }

        [Column("NO_OF_DISEASED_VESSEL_S_50")]
        [StringLength(10)]
        [Unicode(false)]
        public string? NoOfDiseasedVesselS50 { get; set; }

        [Column("VESSEL_OF_REVASCULARIZATION")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VesselOfRevascularization { get; set; }

        [Column("CULPRIT")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Culprit { get; set; }

        [Column("TREATMENT_OF_NONCULPRIT")]
        public double? TreatmentOfNonculprit { get; set; }

        [Column("TIMI_FLOW_PRE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? TimiFlowPre { get; set; }

        [Column("LESION_CONFIGURATION")]
        [StringLength(10)]
        [Unicode(false)]
        public string? LesionConfiguration { get; set; }

        [Column("BIFURCATIONAL_LESION")]
        public double? BifurcationalLesion { get; set; }

        [Column("TRIFURCATIONAL_LESION")]
        public double? TrifurcationalLesion { get; set; }

        [Column("CALCIFIC_LESION")]
        public double? CalcificLesion { get; set; }

        [Column("CTO")]
        public double? Cto { get; set; }

        [Column("TORTIOUS_VESSEL")]
        public double? TortiousVessel { get; set; }

        [Column("TIMI_FLOW_POST")]
        [StringLength(10)]
        [Unicode(false)]
        public string? TimiFlowPost { get; set; }

        [Column("TYPE_OF_WIRE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? TypeOfWire { get; set; }

        [Column("HEAVNESS_OF_WIRE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? HeavnessOfWire { get; set; }

        [Column("BUDDY_WIRE")]
        public double? BuddyWire { get; set; }

        [Column("FAILED_WIRING")]
        public double? FailedWiring { get; set; }

        [Column("THROMBUS_ASPIRATION")]
        public double? ThrombusAspiration { get; set; }

        [Column("ANCHOR_BALLOON")]
        public double? AnchorBalloon { get; set; }

        [Column("PREDILATATION")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Predilatation { get; set; }

        [Column("MAX_PRESSURE_OF_BALLOON")]
        [StringLength(500)]
        [Unicode(false)]
        public string? MaxPressureOfBalloon { get; set; }

        [Column("POSTDILATATION")]
        public double? Postdilatation { get; set; }

        [Column("MAX_PRESSURE_OF_POST_BALLOON")]
        [StringLength(500)]
        [Unicode(false)]
        public string? MaxPressureOfPostBalloon { get; set; }

        [Column("DIAMETER_OF_PREDILATATION")]
        [StringLength(500)]
        [Unicode(false)]
        public string? DiameterOfPredilatation { get; set; }

        [Column("DIAMETER_OF_POSTDILATATION_BAL")]
        [StringLength(500)]
        [Unicode(false)]
        public string? DiameterOfPostdilatationBal { get; set; }

        [Column("TYPE_STENT")]
        [StringLength(20)]
        [Unicode(false)]
        public string? TypeStent { get; set; }

        [Column("DIAMETER_OF_STENT")]
        [StringLength(500)]
        [Unicode(false)]
        public string? DiameterOfStent { get; set; }

        [Column("LENGHT_OF_STENT")]
        [StringLength(25)]
        [Unicode(false)]
        public string? LenghtOfStent { get; set; }

        [Column("DIAMETER_OF_2ND_STENT")]
        [StringLength(500)]
        [Unicode(false)]
        public string? DiameterOf2ndStent { get; set; }

        [Column("LENGHT_OF_2ND_STENT")]
        [StringLength(25)]
        [Unicode(false)]
        public string? LenghtOf2ndStent { get; set; }

        [Column("OVERLAPP_IN_MAIN_VESSEL")]
        public double? OverlappInMainVessel { get; set; }

        [Column("BIFURCATIONAL_TECHNIQUE")]
        [StringLength(30)]
        [Unicode(false)]
        public string? BifurcationalTechnique { get; set; }

        [Column("TYPE_2ND_STENT")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Type2ndStent { get; set; }

        [Column("TYPE_3RD_STENT")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Type3rdStent { get; set; }

        [Column("DIAMETER_OF_3RD_STENT")]
        [StringLength(500)]
        [Unicode(false)]
        public string? DiameterOf3rdStent { get; set; }

        [Column("LENGHT_OF_3RD_STENT")]
        [StringLength(25)]
        [Unicode(false)]
        public string? LenghtOf3rdStent { get; set; }

        [Column("TYPE_4TH_STENT")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Type4thStent { get; set; }

        [Column("DIAMETER_OF_4TH_STENT")]
        [StringLength(500)]
        [Unicode(false)]
        public string? DiameterOf4thStent { get; set; }

        [Column("LENGHT_OF_4TH_STENT")]
        [StringLength(25)]
        [Unicode(false)]
        public string? LenghtOf4thStent { get; set; }

        [Column("TYPE_5TH_STENT")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Type5thStent { get; set; }

        [Column("DIAMETER_OF_5TH_STENT")]
        [StringLength(500)]
        [Unicode(false)]
        public string? DiameterOf5thStent { get; set; }

        [Column("LENGHT_OF_5TH_STENT")]
        [StringLength(25)]
        [Unicode(false)]
        public string? LenghtOf5thStent { get; set; }

        [Column("TYPE_6TH_STENT")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Type6thStent { get; set; }

        [Column("DIAMETER_OF_6TH_STENT")]
        [StringLength(500)]
        [Unicode(false)]
        public string? DiameterOf6thStent { get; set; }

        [Column("LENGHT_OF_6TH_STENT")]
        [StringLength(25)]
        [Unicode(false)]
        public string? LenghtOf6thStent { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL", TypeName = "numeric(8, 0)")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("PciReports")]
        public Pat? Pat { get; set; }
    }
}
