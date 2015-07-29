using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace StockMarket.UI
{
    class ListViewColumnSorter: IComparer
    {
        private int ColumnToSort;// ָ�������ĸ�������
        private SortOrder OrderOfSort;// ָ������ķ�ʽ
        private CaseInsensitiveComparer ObjectCompare;// ����CaseInsensitiveComparer�����
        public ListViewColumnSorter(int column)// ���캯��
        {
            ColumnToSort = column;// Ĭ�ϰ���һ������
            OrderOfSort = SortOrder.Ascending;// ����ʽΪ������
            ObjectCompare = new CaseInsensitiveComparer();// ��ʼ��CaseInsensitiveComparer�����
        }
        // ��дIComparer�ӿ�.
        // <returns>�ȽϵĽ��.�����ȷ���0�����x����y����1�����xС��y����-1</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;
            // ���Ƚ϶���ת��ΪListViewItem����
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;
            // �Ƚ�
            string X_TEXT, Y_TEXT;
            X_TEXT = listviewX.SubItems[ColumnToSort].Text;
            Y_TEXT = listviewY.SubItems[ColumnToSort].Text;
            int X_INT, Y_INT;
            try
            {
                X_INT = Int32.Parse(X_TEXT);
                Y_INT = Int32.Parse(Y_TEXT);
                compareResult = ObjectCompare.Compare(X_INT, Y_INT);
            }
            catch
            {
                compareResult = ObjectCompare.Compare(X_TEXT, Y_TEXT);
            }
            // ��������ıȽϽ��������ȷ�ıȽϽ��
            if (OrderOfSort == SortOrder.Ascending)
            {
                // ��Ϊ��������������ֱ�ӷ��ؽ��
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // ����Ƿ�����������Ҫȡ��ֵ�ٷ���
                return (-compareResult);
            }
            else
            {
                // �����ȷ���0
                return 0;
            }
        }
        /// ��ȡ�����ð�����һ������.
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }
        /// ��ȡ����������ʽ.
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}
