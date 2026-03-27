using System;
using System.Drawing;
using System.Windows.Forms;

namespace MortgageCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // 驗證房屋總價
            if (!double.TryParse(txtHousePrice.Text, out double housePrice) || housePrice <= 0)
            {
                MessageBox.Show("請輸入有效的房屋總價。", "輸入錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 驗證自備款比例
            if (!double.TryParse(txtDownPayment.Text, out double downPaymentPercent) || downPaymentPercent < 0 || downPaymentPercent >= 100)
            {
                MessageBox.Show("請輸入有效的自備款比例（0 ~ 99）。", "輸入錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 驗證年利率
            if (!double.TryParse(txtInterestRate.Text, out double annualRate) || annualRate <= 0)
            {
                MessageBox.Show("請輸入有效的年利率。", "輸入錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 驗證貸款年限
            if (!int.TryParse(txtLoanTerm.Text, out int loanYears) || loanYears <= 0)
            {
                MessageBox.Show("請輸入有效的貸款年限。", "輸入錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 驗證寬限期
            int gracePeriodYears = 0;
            if (txtGracePeriod.Text.Trim() != "" && txtGracePeriod.Text.Trim() != "0")
            {
                if (!int.TryParse(txtGracePeriod.Text, out gracePeriodYears) || gracePeriodYears < 0 || gracePeriodYears >= loanYears)
                {
                    MessageBox.Show("請輸入有效的寬限期（需小於貸款年限）。", "輸入錯誤",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 計算貸款總金額
            double downPayment = housePrice * downPaymentPercent / 100;
            double loanAmount = housePrice - downPayment;

            // 月利率
            double monthlyRate = annualRate / 100 / 12;

            // 還款總月數（扣除寬限期後的本息還款期）
            int totalMonths = loanYears * 12;
            int graceMonths = gracePeriodYears * 12;
            int repayMonths = totalMonths - graceMonths;

            // 每月應繳（本 + 息），本息均攤公式
            double monthlyPayment = 0;
            if (monthlyRate == 0)
            {
                monthlyPayment = loanAmount / repayMonths;
            }
            else
            {
                monthlyPayment = loanAmount * monthlyRate * Math.Pow(1 + monthlyRate, repayMonths)
                                 / (Math.Pow(1 + monthlyRate, repayMonths) - 1);
            }

            // 首期利息與首期本金
            double firstInterest;
            double firstPrincipal;

            if (graceMonths > 0)
            {
                // 寬限期內只繳利息
                firstInterest = loanAmount * monthlyRate;
                firstPrincipal = 0;
            }
            else
            {
                firstInterest = loanAmount * monthlyRate;
                firstPrincipal = monthlyPayment - firstInterest;
            }

            // 總利息支出
            double totalInterest;
            double totalRepayment;

            if (graceMonths > 0)
            {
                double graceTotalInterest = loanAmount * monthlyRate * graceMonths;
                double repayTotalPayment = monthlyPayment * repayMonths;
                totalRepayment = graceTotalInterest + repayTotalPayment;
                totalInterest = totalRepayment - loanAmount;
            }
            else
            {
                totalRepayment = monthlyPayment * totalMonths;
                totalInterest = totalRepayment - loanAmount;
            }

            // 顯示結果（含千分位逗號與小數點後兩位）
            lblLoanAmount.Text = $"NT$ {loanAmount:N2}";
            lblMonthlyPayment.Text = $"NT$ {monthlyPayment:N2}";
            lblFirstInterest.Text = $"NT$ {firstInterest:N2}";
            lblFirstPrincipal.Text = $"NT$ {firstPrincipal:N2}";
            lblTotalInterest.Text = $"NT$ {totalInterest:N2}";
            lblTotalRepayment.Text = $"NT$ {totalRepayment:N2}";

            panelResult.Visible = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtHousePrice.Text = "10000000";
            txtDownPayment.Text = "20";
            txtInterestRate.Text = "2.15";
            txtLoanTerm.Text = "30";
            txtGracePeriod.Text = "0";
            panelResult.Visible = false;
        }
    }
}