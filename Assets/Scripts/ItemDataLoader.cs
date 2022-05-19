using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDataLoader : MonoBehaviour
{
    public List<string> originalstr = null;
    public List<Dictionary<int, string>> ItemFileOpen(string filepath, out string classname)
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
            Debug.Log("여기서 나감1");
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
                    flag = true;//여는중괄호가 나오면 닫는중괄호가 나올때까지 앞으로 나오는 , 는 그냥 집어넣는다.  
                    continue;
                }
                else if (str[i] == ',')
                {
                    if (!flag)
                    {
                        temp[index] = '\0';
                        string tt = string.Join("", temp);
                        columsdic.Add(Dicindex++, tt.Split('\0')[0]);
                        temp = new char[str.Length];
                        index = 0;
                        continue;
                    }
                }
                else if (i == str.Length - 1)
                {
                    if (str[i] != '}')
                        temp[index++] = str[i];

                    string tt = string.Join("", temp);
                    columsdic.Add(Dicindex++, tt.Split('\0')[0]);
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

            if (int.TryParse(columsdic[0], out RowNum))//첫번째가 숫자가 아니면 해당 행은 열제목 행이다. 리스트에 넣지 않는다.
            {
                data.Add(columsdic);
            }
            else
            {
                //classname = columsdic[0].Split('_')[0];
            }

        }
        int a = 10;
        //Debug.Log("여기서 나감2");
        return data;
    }

    //불러와진 아이템 정보들을 이용해 아이템 노드를 만들어 준다.
    public void ItemInfo_Road(string classname)
    {
        string filepath = UnityEngine.Application.persistentDataPath + $"/{classname}_Relation.csv";
        Debug.Log(filepath);
        List<Dictionary<int, string>> datalist = ItemFileOpen(filepath, out classname);
        //nodeobj.gameObject.SetActive(true);

        int ItemCode;
        string ItemName;
        string SpriteName;
        int SpriteIndex;




        //List<SkillNode> parentlist;
        //List<SkillNode> childlist;

        //List<SkillNode> tempnodelist = new List<SkillNode>();

        //for (int i = 0; i < datalist.Count; i++)
        //{
        //    string[] indexstr = datalist[i][(int)EnumTypes.SkillTreeColums.Index].Split(',');
        //    int indexX = -1;
        //    int indexY = -1;
        //    int.TryParse(indexstr[0], out indexX);
        //    int.TryParse(indexstr[1], out indexY);
        //    int.TryParse(datalist[i][(int)EnumTypes.SkillTreeColums.SkillDamage], out damage);
        //    int.TryParse(datalist[i][(int)EnumTypes.SkillTreeColums.ReauireLevel], out level);
        //    int.TryParse(datalist[i][(int)EnumTypes.SkillTreeColums.SkillRank], out rank);
        //    skillname = datalist[i][(int)EnumTypes.SkillTreeColums.SkillName];
        //    explain = datalist[i][(int)EnumTypes.SkillTreeColums.SkillExplain];


        //    SkillNode copyobj = GameObject.Instantiate<SkillNode>(nodeobj);
        //    copyobj.name = skillname;
        //    //copyobj.transform.parent = SkillNodeObject.transform.parent;
        //    copyobj.Init(classname, skillname, rank, damage, level, explain);
        //    copyobj.P_NodeActive = false;
        //    tempnodelist.Add(copyobj);
        //    skillnodes.Add(copyobj);

        //    panel.SetTreeNode(new Vector2Int(indexX, indexY), copyobj);//만들어준 슼킬 노드를 바로 슬롯에 장착시켜준다.

        //}

        //노드들을 다 만들어 준 다음에 연결정보를 읽어와서 노드들을 연결해준다.
        //for (int i = 0; i < datalist.Count; i++)
        //{
        //    ParseRelation(tempnodelist, tempnodelist[i], datalist[i][(int)EnumTypes.SkillTreeColums.Child]);
        //}

        //if (skillslots == null)
        //    skillslots = panel.GetSkillSlotList;

        //for (int y = 0; y < skillslots.GetLength(0); y++)
        //{
        //    for (int x = 0; x < skillslots.GetLength(1); x++)
        //    {
        //        if (skillslots[y, x].SetNode == null)
        //        {
        //            skillslots[y, x].gameObject.SetActive(false);
        //        }
        //    }
        //}

        //nodeobj.gameObject.SetActive(false);
        //for(int i=0;i<skillslots)
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
