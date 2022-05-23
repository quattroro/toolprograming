using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



///////////////////////////////////////////////////////////////////////////////////////
//��������������������������������������������������������
//�� 0�� 1�� 0�� 0�� 2�� 0�� 0�� 2�� 0�� 
//��������������������������������������������������������
//��������������������������������������������������������
//�� 1�� 0�� 0�� 2�� 0�� 0�� 2�� 0�� 0�� 
//��������������������������������������������������������
//��������������������������������������������������������
//�� 0�� 0�� 1�� 0�� 0�� 2�� 0�� 0�� 2�� 
//��������������������������������������������������������
//�� �������� �����ǰ� ��� ���� ����� ���;� �Ѵ�. 
//�����Ǹ� 9�� ũ���� �����Ƿ� �����ϴ°��� �ƴ� �������� ���ۺ��� �������� �������θ� �����Ѵ�.
//��������������������������������������������
//�� 1�� 0�� 0�� 2�� 0�� 0�� 2�� //�ʿ���� �κ��� ����� �ʿ��� �κи� ����
//��������������������������������������������
///////////////////////////////////////////////////////////////////////////////////////

//���� �ϳ��� ���� ������
[System.Serializable]
public class CraftRecipe
{
    
    public int[] Pattern;
    public int result;
    public int stock;
    public int patternsize;


    //������ ������ ����� �ش�.
    public CraftRecipe(int result, int resultstock,int[] recipe)
    {
        this.result = result;
        this.stock = resultstock;
        this.patternsize = 0;
        int index = 0;
        int startindex = -1;
        int endindex = -1;
        for(int i=0; i<recipe.Length;i++)
        {
            if (recipe[i] != 0 && endindex <= i)
            {
                if (startindex == -1)
                    startindex = i;
                endindex = i; 
            }
        }
        patternsize = endindex - startindex + 1;


        Pattern = new int[patternsize];
        for (int i = startindex; i <= endindex; i++)
        {
            Pattern[index++] = recipe[i];
        }

        //studentList.Sort((x1, x2) => x2.Weight.CompareTo(x1.Weight));
    }

    public bool IsEqual(int[] pattern)
    {
        if (this.patternsize != pattern.Length)
        {
            Debug.Log("�߸��� ��");
            return false;
        }
        for(int i=0;i<patternsize;i++)
        {
            if(this.Pattern[i]!=pattern[i])
            {
                return false;
            }
        }
        return true;
    }


    static public int[] GetPattern(int[] input)
    {
        int[] temp;

        int size = 0;
        int index = 0;
        int startindex = -1;
        int endindex = -1;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != 0 && endindex <= i)
            {
                if (startindex == -1)
                    startindex = i;
                endindex = i;
            }
        }
        size = endindex - startindex + 1;

        temp = new int[size];
        for (int i = startindex; i <= endindex; i++)
        {
            temp[index++] = input[i];
        }

        return temp;
    }

    //�Է¹��� �����Ǹ� �Ȱ��� �������� �ٲ۴����� 1�������� ������ ũ�⸦ ���ϰ� 2�������� ������ ���Ͽ� �ش� �����Ǹ� Ž���Ѵ�. 1��° Ž���� �Ҷ��� ����Ž���� �̿��Ѵ�.
    static public BaseNode SearchRecipe(List<CraftRecipe> recipelist, int[] input)
    {
        BaseNode node = null;
        int[] inputpattern = GetPattern(input);

        //���� ũ���� �����ǵ��� �̾Ƽ� ���Ѵ�.
        List<CraftRecipe> temparr =  recipelist.FindAll(x => x.patternsize == inputpattern.Length);

        for(int i=0;i<temparr.Count;i++)
        {
            if (temparr[i].IsEqual(inputpattern))
            {
                Debug.Log($"ã�� {temparr[i].result}");

                node = GameObject.Instantiate<BaseNode>(ItemDataLoader.Instance.itemnodes.Find(x => x.GetItemID() == temparr[i].result));
                node.ChangeStack(0);
                node.ChangeStack(temparr[i].stock);
                node.NodeIsActive = true;
                return node;
                //return temparr[i].result;
            }
        }

        Debug.Log($"��ã��");
        return node;
    }


}

public class CraftDataLoader : MonoBehaviour
{
    public List<string> originalstr = null;
    public string FilePath;

    public List<BaseNode> itemnodes = new List<BaseNode>();
    public Sprite[] sprites = null;

    public List<CraftRecipe> recipelist = new List<CraftRecipe>();


    public List<Dictionary<int, string>> CraftFileOpen(string filepath, out string classname)
    {
        originalstr.Clear();
        List<Dictionary<int, string>> data = new List<Dictionary<int, string>>();
        string FilePath = filepath;
        Debug.Log($"road {FilePath}");
        classname = null;

        //if (OpenDialog.ShowDialog() == DialogResult.OK)
        //{
        //    if ((openStream = OpenDialog.OpenFile()) != null)
        //    {
        //        //return OpenDialog.FileName;
        //        FilePath = OpenDialog.FileName;
        //    }
        //}
        if (FilePath == null)
        {
            Debug.Log("���⼭ ����1");
            return null;
        }


        FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);

        StreamReader sr = new StreamReader(fs);

        while (true)
        {
            string str = sr.ReadLine();
            originalstr.Add(str);

            if (str == null || str.Length == 0)
            {
                break;
            }
            char[] temp = new char[str.Length];
            bool flag = false;

            Dictionary<int, string> columsdic = new Dictionary<int, string>();
            int Dicindex = 0;
            int index = 0;
            //string temp;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '{')
                {
                    flag = true;//�����߰�ȣ�� ������ �ݴ��߰�ȣ�� ���ö����� ������ ������ , �� �׳� ����ִ´�.  
                    continue;
                }
                else if (str[i] == ',')//,�� ������ �ش� �ε������� , string���� ��ȯ�ؼ� columsdic �� �־��ش�.
                {
                    if (!flag)
                    {
                        temp[index] = '\0';
                        string tt = string.Join("", temp);//
                        columsdic.Add(Dicindex++, tt.Split('\0')[0]);//�׳� new string() �����ڸ� �̿��ϸ� char�迭�� ũ�⸸ŭ �ڿ� \0���� �ʱ�ȭ�� ���ڿ��� �������������.

                        temp = new char[str.Length];
                        index = 0;
                        continue;
                    }
                }
                else if (i == str.Length - 1)//�޾ƿ� �� ���� ������ �����϶��� ��ǥ�� ���� ������ ���ش�.
                {
                    if (str[i] != '}')//���� �������� } �� ���������� }���ڵ� �־��ش�.
                        temp[index++] = str[i];

                    temp[index] = '\0';
                    string tt = string.Join("", temp);
                    columsdic.Add(Dicindex++, tt.Split('\0')[0]);//�׳� new string() �����ڸ� �̿��ϸ� char�迭�� ũ�⸸ŭ �ڿ� \0���� �ʱ�ȭ�� ���ڿ��� �������������.

                    temp = new char[str.Length];
                    index = 0;
                    break;
                }
                else if (str[i] == '}')
                {
                    flag = false;
                    continue;
                }


                temp[index++] = str[i];

            }


            int RowNum = 0;

            if (int.TryParse(columsdic[0], out RowNum))//ù��°�� ���ڰ� �ƴϸ� �ش� ���� ������ ���̴�. ����Ʈ�� ���� �ʴ´�.
            {
                data.Add(columsdic);
            }
            else
            {
                //classname = columsdic[0].Split('_')[0];
            }

        }
        return data;
    }

    public void CraftInfo_Road(string classname)
    {
        Debug.Log(FilePath);
        string temppath = Application.streamingAssetsPath + FilePath;//��Ÿ���� �б⸸ ����
        List<Dictionary<int, string>> datalist = CraftFileOpen(temppath, out classname);
        int[] recipe = new int[9];
        int result;
        int stock;
        
        //List<CraftRecipe> recipelist = new List<CraftRecipe>();

        for (int i = 0; i < datalist.Count; i++)
        {
            int.TryParse(datalist[i][(int)EnumTypes.RecipeCollums.Count], out stock);
            int.TryParse(datalist[i][(int)EnumTypes.RecipeCollums.ResultItem], out result);
            for (EnumTypes.RecipeCollums j = EnumTypes.RecipeCollums.Slot1;j<=EnumTypes.RecipeCollums.Slot9;j++)
            {
                int.TryParse(datalist[i][(int)j], out recipe[(int)j - 3]);
            }
            recipelist.Add(new CraftRecipe(result, stock, recipe));
        }
        recipelist.Sort((x1, x2) => x1.patternsize.CompareTo(x2.patternsize));//ũ������� �������� ����

        ItemCraftTable.Instance.recipelist = recipelist;

    }



    void Start()
    {
        CraftInfo_Road(FilePath);
    }


}
