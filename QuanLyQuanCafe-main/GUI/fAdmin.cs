﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource staffList = new BindingSource();
        public Account loginAccount;
        public fAdmin(Account acc)
        {
            InitializeComponent();
            this.loginAccount = acc;
            LoadData();
        }
        void LoadData()
        {
            dtgvAccount.DataSource = accountList;
            dtgvFood.DataSource = foodList;
            dtgvCategory.DataSource = categoryList;
            dtgvAccount.DataSource = accountList;
            dtgvTable.DataSource = tableList;
            dtgvStaff.DataSource = staffList;
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadListCategory();
            LoadAccount();
            LoadCategoryIntoCombobox(cbFoodCategory);
            LoadListNameStaff(cbNameStaff);
            LoadListNameStaff(cbNameStafff);
            LoadListTable();
            LoadListStaff();
            LoadCbStatus();
            LoadSumBill();
            AddFoodBinding();
            AddAccountBinding();
            AddCategoryBinding();
            AddTableBinding();
            AddStaffBiding();

        }
        void LoadCbStatus()
        {
            cbTableStatus.Items.Add(0);
            cbTableStatus.Items.Add(1);
            cbStatusStaff.Items.Add(0);
            cbStatusStaff.Items.Add(1);
        }
        void LoadSumBill()
        {
            txbSum.Text = BillDAO.Instance.SumBill(dtpkFromDate.Value.AddMonths(0).AddDays(-1), dtpkToDate.Value.AddMonths(0).AddDays(1)).ToString();
        }
        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
            txbPass.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Password", true, DataSourceUpdateMode.Never));
            cbNameStaff.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Staff_name", true, DataSourceUpdateMode.Never));
        }
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void AddCategoryBinding()
        {
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }
        void AddTableBinding()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            cbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }
        void AddStaffBiding()
        {
            cbNameStafff.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbIDStaff.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbPhone.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Sdt", true, DataSourceUpdateMode.Never));
            txbAddress.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Address", true, DataSourceUpdateMode.Never));
            cbSex.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Sex", true, DataSourceUpdateMode.Never));
            txbSalary.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Salary", true, DataSourceUpdateMode.Never));
            cbStatusStaff.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }
        void LoadListTable()
        {
            tableList.DataSource = TableDAO.Instance.GetListTable();
        }
        void LoadListNameStaff(ComboBox cb)
        {
            cb.DataSource = StaffDAO.Instance.GetListStaff();
            cb.DisplayMember = "Name";
        }
        void LoadAccountList()
        {
            string query = "select * from dbo.Account";
            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { "staff" });
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void LoadListStaff()
        {
            staffList.DataSource = StaffDAO.Instance.GetListStaff();
        }
        //Account
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void AddAccount(string userName, string displayName,string pass, int type,int idstaff)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName,pass, type,idstaff))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
            LoadAccount();
        }
        void EditAccount(string userName, string displayName, string pass, int type, int id_staff)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, pass, type, id_staff))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }
            LoadAccount();
        }
        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Vui lòng đừng xóa chính bạn chứ");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }

            LoadAccount();
        }
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (txbUserName.Text == "" ||txbPass.Text=="")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
            string userName = txbUserName.Text;
            List<Account> AccountList = AccountDAO.Instance.GetListAccountt();
            foreach (Account item in AccountList)
            {
                if (item.UserName == userName)
                {
                    MessageBox.Show("Tài khoản đã tồn tại!");
                    return;
                }
            }
            
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;
            string pass = txbPass.Text;
            int id_staff = (cbNameStaff.SelectedItem as Staff).Id;
            AddAccount(userName, displayName,pass, type,id_staff);
        }
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            List<Account> AccountList = AccountDAO.Instance.GetListAccountt();
            try
            {
                DeleteAccount(userName);
            }
            catch { }
        }
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;
            int id_staff = (cbNameStaff.SelectedItem as Staff).Id;
            string pass = txbPass.Text;
            if (userName ==""||pass=="")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            List<Account> AccountList = AccountDAO.Instance.GetListAccountt();
            foreach (Account item in AccountList)
            {
                if (item.Id_staff != id_staff)
                {
                    if (item.UserName == userName)
                    {
                        MessageBox.Show("Tài khoản đã tồn tại!");
                        return;
                    }
                }
            }
            EditAccount(userName, displayName, pass, type, id_staff);
        }
        private void btnNhap_Click(object sender, EventArgs e)
        {
            txbDisplayName.Text = "";
            txbUserName.Text = "";
            cbNameStaff.Text = "";
            txbPass.Text = "";
        }

        //Food
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);
            return listFood;
        }
        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
            txbSearchFoodName.Text = "";
        }
        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == cateogory.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (txbFoodName.Text == "" || cbFoodCategory.SelectedItem.ToString() == "" || nmFoodPrice.Value == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                string name = txbFoodName.Text;
                List<Food> FoodList = FoodDAO.Instance.GetListFood();
                foreach (Food item in FoodList)
                {
                    if (item.Name == name)
                    {
                        MessageBox.Show("Món đã tồn tại!");
                        return;
                    }
                }
                int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
                float price = (float)nmFoodPrice.Value;

                if (FoodDAO.Instance.InsertFood(name, categoryID, price))
                {
                    MessageBox.Show("Thêm món thành công");
                    LoadListFood();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm món");
                }
            }
                          
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            if (price < 0)
            {
                MessageBox.Show("Hãy nhập đúng giá tiền!");
                return;
            }
            int id = Convert.ToInt32(txbFoodID.Text);
            List<Food> FoodList = FoodDAO.Instance.GetListFood();
            foreach (Food item in FoodList)
            {
                if (item.ID != id)
                {
                    if (item.Name == name)
                    {
                        MessageBox.Show("Món đã tồn tại!");
                        return;
                    }
                }
            }
            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa món");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            try
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa món này không?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)

                    if (FoodDAO.Instance.DeleteFood(id))
                    {
                        MessageBox.Show("Xóa món thành công");
                        LoadListFood();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi xóa món");
                    }
            }
            catch { MessageBox.Show("Không thể xóa món!"); }
        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        



        //Bill
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        private void btnFristBillPage_Click(object sender, EventArgs e)
        {
            txbPageBill.Text = "1";
        }

        private void btnLastBillPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);

            int lastPage = sumRecord / 10;

            if (sumRecord % 10 != 0)
                lastPage++;

            txbPageBill.Text = lastPage.ToString();
        }

        private void txbPageBill_TextChanged(object sender, EventArgs e)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(txbPageBill.Text));
        }

        private void btnPrevioursBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);

            if (page > 1)
                page--;

            txbPageBill.Text = page.ToString();
        }

        private void btnNextBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);

            if (page < sumRecord)
                page++;

            txbPageBill.Text = page.ToString();
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QuanLyQuanCafeDataSet2.USP_GetListBillByDateForReport' table. You can move, or remove it, as needed.
            //this.USP_GetListBillByDateForReportTableAdapter.Fill(this.QuanLyQuanCafeDataSet2.USP_GetListBillByDateForReport, dtpkFromDate.Value, dtpkToDate.Value);           
            //this.rpViewer.RefreshReport();
        }


        //Category
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if ( txbCategoryName.Text=="")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }    
            string name = txbCategoryName.Text;
            List<Category> CategoryList = CategoryDAO.Instance.GetListCategory();
            foreach (Category item in CategoryList)
            {
                if (item.Name == name)
                {
                    MessageBox.Show("Danh mục đã tồn tại!");
                    return;
                }    
            }    
            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công");
                LoadListCategory();
                LoadCategoryIntoCombobox(cbFoodCategory);
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txbCategoryID.Text);
                if (MessageBox.Show("Bạn có chắc muốn xóa danh mục này không?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)

                    if (CategoryDAO.Instance.DeleteCategory(id))
                    {
                        MessageBox.Show("Xóa danh mục thành công");
                        LoadListCategory();
                        LoadListFood();
                        LoadCategoryIntoCombobox(cbFoodCategory);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi xóa danh mục");
                    }
            }
            catch {
                MessageBox.Show("Không thể xóa danh mục!");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            List<Category> CategoryList = CategoryDAO.Instance.GetListCategory();
            foreach (Category item in CategoryList)
            {
                if (item.Name == name)
                {
                    MessageBox.Show("Hãy nhập tên mới!");
                    return;
                }
            }
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Sửa danh mục thành công");
                LoadListCategory();
                LoadCategoryIntoCombobox(cbFoodCategory);
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa danh mục");
            }
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }
        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            string name = txbFindCategory.Text;
            categoryList.DataSource = CategoryDAO.Instance.SearchCategoryByName(name);
            txbFindCategory.Text = "";
        }


        //Table
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            if (txbTableName.Text=="")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }    
            string name = txbTableName.Text;
            List<Table> TableList = TableDAO.Instance.GetListTable();
            foreach (Table item in TableList)
            {
                if (item.Name == name)
                {
                    MessageBox.Show("Tên bàn đã tồn tại!");
                    return;
                }
            }
            int status = Int32.Parse(cbTableStatus.SelectedItem.ToString());
            if (TableDAO.Instance.InsertTable(name, status))
            {
                MessageBox.Show("Thêm bàn ăn thành công");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm bàn ăn");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txbTableID.Text);
                if (MessageBox.Show("Bạn có chắc muốn xóa bàn này không?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)

                    if (TableDAO.Instance.DeleteTable(id))
                    {
                        MessageBox.Show("Xóa bàn thành công");
                        LoadListTable();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi xóa bàn");
                    }
            }
            catch { }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            int id = Convert.ToInt32(txbTableID.Text);
            int status = Convert.ToInt32(cbTableStatus.SelectedItem.ToString());
            List<Table> TableList = TableDAO.Instance.GetListTable();
            foreach (Table item in TableList)
            {
                if (item.ID != id)
                {
                    if (item.Name == name)
                    {
                        MessageBox.Show("Vui lòng nhập tên mới!");
                        return;
                    }
                }
            }
            if (TableDAO.Instance.UpdateTable(id, name, status))
            {
                MessageBox.Show("Sửa thông tin bàn thành công");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thông tin bàn");
            }
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }


        //Staff
        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
                int id = Convert.ToInt32(txbIDStaff.Text);
                if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này không?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    if (StaffDAO.Instance.DeleteStaff(id))
                    {
                        MessageBox.Show("Xóa nhân viên thành công");
                        LoadListStaff();
                    }
        }
        /*public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }*/
        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            string name = cbNameStafff.Text;
            string sdt = txbPhone.Text;
            List<Staff> StaffList = StaffDAO.Instance.GetListStaff();
            foreach (Staff item in StaffList)
            {
                if (item.Name == name)
                {
                    MessageBox.Show("Nhân viên đã tồn tại!");
                    return;
                }
                if (item.Sdt==sdt)
                {
                    MessageBox.Show("Số điện thoại này đã có chủ!");
                    return;
                }    
            }
            string phone = txbPhone.Text;
            string address = txbAddress.Text;
            string sex = cbSex.SelectedItem.ToString();
            int salary = Int32.Parse(txbSalary.Text);
            int status = Int32.Parse(cbStatusStaff.SelectedItem.ToString());

            if (StaffDAO.Instance.InsertStaff(name, phone, address, sex, salary, status))
            {
                MessageBox.Show("Thêm nhân viên thành công");
                LoadListStaff();
            }
        }

        private void btnEditStaff_Click(object sender, EventArgs e)
        {
            string name = cbNameStafff.Text;
            string phone = txbPhone.Text;
            string address = txbAddress.Text;
            string sex = cbSex.SelectedItem.ToString();
            int salary = Convert.ToInt32(txbSalary.Text);
            List<Staff> StaffList = StaffDAO.Instance.GetListStaff();
            foreach (Staff item in StaffList)
            {
                if (item.Id != Int32.Parse(txbIDStaff.Text))
                {
                    if (item.Name == name)
                    {
                        MessageBox.Show("Nhân viên đã tồn tại!");
                        return;
                    }
                    if (item.Sdt == phone)
                    {
                        MessageBox.Show("Số điện thoại này đã có chủ!");
                        return;
                    }
                }
            
            }
            int status = Convert.ToInt32(cbStatusStaff.Text);
            int id = Convert.ToInt32(txbIDStaff.Text);

            if (StaffDAO.Instance.UpdateStaff(id, name, phone, address, sex, salary, status))
            {
                MessageBox.Show("Sửa thông tin nhân viên thành công");
                LoadListStaff();
            }
        }

        private void btnLoadStaff_Click(object sender, EventArgs e)
        {
            LoadListStaff();
        }

        private void btnSearchStaff_Click(object sender, EventArgs e)
        {
            staffList.DataSource = StaffDAO.Instance.SearchStaffByName(txbFindStaff.Text);
            txbFindStaff.Text = "";
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            txbIDStaff.Text = "";
            txbPass.Text = "";
            txbPhone.Text = "";
            txbSalary.Text = "";
            cbSex.Text = "";
            cbNameStafff.Text = "";
            cbStatusStaff.Text = "";
            txbAddress.Text = "";
        }

        private void dtpkFromDate_ValueChanged(object sender, EventArgs e)
        {
            LoadSumBill();
        }

        private void dtpkToDate_ValueChanged(object sender, EventArgs e)
        {
            LoadSumBill();
        }

        private void exportPDF_Click(object sender, EventArgs e)
        {
            BaseFont bf = BaseFont.CreateFont("c:\\fonts\\vuArial.ttf",
                                   BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            // khỏi tạo font chữ
            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 15);
            if (dtgvBill.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Không thể ghi dữ liệu tới ổ đĩa. Mô tả lỗi:" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dtgvBill.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;


                            foreach (DataGridViewColumn column in dtgvBill.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, font));
                                pdfTable.AddCell(cell);

                            }

                            foreach (DataGridViewRow row in dtgvBill.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.Value != null)
                                    {
                                        PdfPCell t = new PdfPCell(new Phrase(cell.Value.ToString(), font));
                                        pdfTable.AddCell(t);
                                    }
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();

                                Paragraph nameShop = new Paragraph("Cafe Sinh viên", font);
                                nameShop.Alignment = Element.ALIGN_LEFT;
                                pdfDoc.Add(nameShop);
                                pdfDoc.Add(new Paragraph("\n"));

                                Paragraph addrShop = new Paragraph("Địa chỉ: 1-Võ Văn Ngân, Linh Trung, Thủ Đức", font);
                                addrShop.Alignment = Element.ALIGN_LEFT;
                                pdfDoc.Add(addrShop);
                                pdfDoc.Add(new Paragraph("\n"));

                                Paragraph telShop = new Paragraph("Số điện thoại: 099", font);
                                telShop.Alignment = Element.ALIGN_LEFT;
                                pdfDoc.Add(telShop);
                                pdfDoc.Add(new Paragraph("\n"));

                                Paragraph numberBill = new Paragraph("Số lượng hóa đơn:", font);
                                numberBill.Alignment = Element.ALIGN_LEFT;
                                pdfDoc.Add(numberBill);
                                pdfDoc.Add(new Paragraph("\n"));

                                Paragraph totalMoney = new Paragraph("Tổng thu:", font);
                                totalMoney.Alignment = Element.ALIGN_LEFT;
                                pdfDoc.Add(totalMoney);
                                pdfDoc.Add(new Paragraph("\n"));

                                Paragraph para = new Paragraph("Thống kê " + dtpkFromDate.Value.ToString("dd/MM/yyyy hh:mm:ss") + " đến " + dtpkToDate.Value.ToString("dd/MM/yyyy hh:mm:ss"), font);
                                para.Alignment = Element.ALIGN_CENTER;
                                pdfDoc.Add(para);
                                pdfDoc.Add(new Paragraph("\n"));
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Dữ liệu Export thành công!!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Mô tả lỗi :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có bản ghi nào được Export!!!", "Info");
            }
        }
    }
 
}
