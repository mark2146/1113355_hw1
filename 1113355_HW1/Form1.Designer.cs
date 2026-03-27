using System;
using System.Drawing;
using System.Windows.Forms;

namespace MortgageCalculator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblHousePrice = new Label();
            this.lblDownPayment = new Label();
            this.lblDownPaymentUnit = new Label();
            this.lblInterestRate = new Label();
            this.lblInterestUnit = new Label();
            this.lblLoanTerm = new Label();
            this.lblLoanTermUnit = new Label();
            this.lblGracePeriod = new Label();
            this.lblGraceUnit = new Label();
            this.txtHousePrice = new TextBox();
            this.txtDownPayment = new TextBox();
            this.txtInterestRate = new TextBox();
            this.txtLoanTerm = new TextBox();
            this.txtGracePeriod = new TextBox();
            this.btnCalculate = new Button();
            this.btnClear = new Button();
            this.panelResult = new Panel();

            // 結果 Labels
            this.lblResultTitle = new Label();
            this.lblLoanAmountTitle = new Label();
            this.lblLoanAmount = new Label();
            this.lblMonthlyPaymentTitle = new Label();
            this.lblMonthlyPayment = new Label();
            this.lblFirstInterestTitle = new Label();
            this.lblFirstInterest = new Label();
            this.lblFirstPrincipalTitle = new Label();
            this.lblFirstPrincipal = new Label();
            this.lblTotalInterestTitle = new Label();
            this.lblTotalInterest = new Label();
            this.lblTotalRepaymentTitle = new Label();
            this.lblTotalRepayment = new Label();

            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────
            this.Text = "個人房貸試算器";
            this.Size = new Size(520, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Microsoft JhengHei", 10F);

            // ── 標題 ─────────────────────────────────────────────
            this.lblTitle.Text = "個人房貸試算器";
            this.lblTitle.Font = new Font("Microsoft JhengHei", 16F, FontStyle.Bold);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Size = new Size(480, 45);
            this.lblTitle.Location = new Point(10, 10);

            // ── 輸入區 helper ─────────────────────────────────────
            int labelX = 30, inputX = 170, unitX = 340;
            int rowH = 45, startY = 65;

            // 房屋總價
            SetLabel(lblHousePrice, "房屋總價：", labelX, startY);
            SetInput(txtHousePrice, "10000000", inputX, startY);
            SetLabel(new Label(), "元", unitX, startY); // dummy, use inline

            var lblPriceUnit = new Label();
            SetLabel(lblPriceUnit, "元 (NT$)", unitX, startY);
            this.Controls.Add(lblPriceUnit);

            // 自備款比例
            SetLabel(lblDownPayment, "自備款比例：", labelX, startY + rowH);
            SetInput(txtDownPayment, "20", inputX, startY + rowH);
            SetLabel(lblDownPaymentUnit, "% (0~99)", unitX, startY + rowH);

            // 年利率
            SetLabel(lblInterestRate, "年利率：", labelX, startY + rowH * 2);
            SetInput(txtInterestRate, "2.15", inputX, startY + rowH * 2);
            SetLabel(lblInterestUnit, "% (例：2.15)", unitX, startY + rowH * 2);

            // 貸款年限
            SetLabel(lblLoanTerm, "貸款年限：", labelX, startY + rowH * 3);
            SetInput(txtLoanTerm, "30", inputX, startY + rowH * 3);
            SetLabel(lblLoanTermUnit, "年", unitX, startY + rowH * 3);

            // 寬限期
            SetLabel(lblGracePeriod, "寬限期（選填）：", labelX, startY + rowH * 4);
            SetInput(txtGracePeriod, "0", inputX, startY + rowH * 4);
            SetLabel(lblGraceUnit, "年（只繳利息）", unitX, startY + rowH * 4);

            // ── 按鈕 ─────────────────────────────────────────────
            int btnY = startY + rowH * 5 + 5;
            this.btnCalculate.Text = "計算";
            this.btnCalculate.Location = new Point(110, btnY);
            this.btnCalculate.Size = new Size(100, 36);
            this.btnCalculate.BackColor = Color.SteelBlue;
            this.btnCalculate.ForeColor = Color.White;
            this.btnCalculate.FlatStyle = FlatStyle.Flat;
            this.btnCalculate.Click += new EventHandler(this.btnCalculate_Click);

            this.btnClear.Text = "清除";
            this.btnClear.Location = new Point(230, btnY);
            this.btnClear.Size = new Size(100, 36);
            this.btnClear.BackColor = Color.Gray;
            this.btnClear.ForeColor = Color.White;
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            // ── 結果 Panel ────────────────────────────────────────
            int panelY = btnY + 50;
            this.panelResult.Location = new Point(20, panelY);
            this.panelResult.Size = new Size(460, 250);
            this.panelResult.BackColor = Color.White;
            this.panelResult.BorderStyle = BorderStyle.FixedSingle;
            this.panelResult.Visible = false;

            this.lblResultTitle.Text = "📊 試算結果";
            this.lblResultTitle.Font = new Font("Microsoft JhengHei", 11F, FontStyle.Bold);
            this.lblResultTitle.Location = new Point(10, 8);
            this.lblResultTitle.Size = new Size(440, 28);

            int r = 40, rH = 33;
            SetResultRow(lblLoanAmountTitle, lblLoanAmount, "貸款總金額：", r);
            SetResultRow(lblMonthlyPaymentTitle, lblMonthlyPayment, "每月應繳金額：", r + rH);
            SetResultRow(lblFirstInterestTitle, lblFirstInterest, "首期利息：", r + rH * 2);
            SetResultRow(lblFirstPrincipalTitle, lblFirstPrincipal, "首期本金：", r + rH * 3);
            SetResultRow(lblTotalInterestTitle, lblTotalInterest, "總利息支出：", r + rH * 4);
            SetResultRow(lblTotalRepaymentTitle, lblTotalRepayment, "總還款金額：", r + rH * 5);

            this.panelResult.Controls.AddRange(new Control[] {
                lblResultTitle,
                lblLoanAmountTitle, lblLoanAmount,
                lblMonthlyPaymentTitle, lblMonthlyPayment,
                lblFirstInterestTitle, lblFirstInterest,
                lblFirstPrincipalTitle, lblFirstPrincipal,
                lblTotalInterestTitle, lblTotalInterest,
                lblTotalRepaymentTitle, lblTotalRepayment
            });

            // ── 加入所有控件 ──────────────────────────────────────
            this.Controls.AddRange(new Control[] {
                lblTitle,
                lblHousePrice, txtHousePrice,
                lblDownPayment, txtDownPayment, lblDownPaymentUnit,
                lblInterestRate, txtInterestRate, lblInterestUnit,
                lblLoanTerm, txtLoanTerm, lblLoanTermUnit,
                lblGracePeriod, txtGracePeriod, lblGraceUnit,
                btnCalculate, btnClear,
                panelResult
            });

            this.ResumeLayout(false);
        }

        private void SetLabel(Label lbl, string text, int x, int y)
        {
            lbl.Text = text;
            lbl.Location = new Point(x, y + 4);
            lbl.Size = new Size(140, 26);
            lbl.TextAlign = ContentAlignment.MiddleRight;
        }

        private void SetInput(TextBox txt, string defaultVal, int x, int y)
        {
            txt.Text = defaultVal;
            txt.Location = new Point(x, y);
            txt.Size = new Size(160, 28);
        }

        private void SetResultRow(Label title, Label value, string titleText, int y)
        {
            title.Text = titleText;
            title.Location = new Point(10, y);
            title.Size = new Size(130, 26);
            title.TextAlign = ContentAlignment.MiddleRight;
            title.Font = new Font("Microsoft JhengHei", 9.5F);

            value.Text = "";
            value.Location = new Point(150, y);
            value.Size = new Size(280, 26);
            value.ForeColor = Color.DarkBlue;
            value.Font = new Font("Microsoft JhengHei", 9.5F, FontStyle.Bold);
        }

        // ── 控件宣告 ─────────────────────────────────────────────
        private Label lblTitle;
        private Label lblHousePrice, lblDownPayment, lblDownPaymentUnit;
        private Label lblInterestRate, lblInterestUnit;
        private Label lblLoanTerm, lblLoanTermUnit;
        private Label lblGracePeriod, lblGraceUnit;
        private TextBox txtHousePrice, txtDownPayment, txtInterestRate;
        private TextBox txtLoanTerm, txtGracePeriod;
        private Button btnCalculate, btnClear;
        private Panel panelResult;
        private Label lblResultTitle;
        private Label lblLoanAmountTitle, lblLoanAmount;
        private Label lblMonthlyPaymentTitle, lblMonthlyPayment;
        private Label lblFirstInterestTitle, lblFirstInterest;
        private Label lblFirstPrincipalTitle, lblFirstPrincipal;
        private Label lblTotalInterestTitle, lblTotalInterest;
        private Label lblTotalRepaymentTitle, lblTotalRepayment;
    }
}