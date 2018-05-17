using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChildManager.Model
{
  public  class ProjectGaoweiobj 
  {
      //主项目
      public int id;
      public string type;

      //子项目
      public int p_id;
      public string content;
      /// <summary>
      /// 孩子id
      /// </summary>
      public int cid;
    }
}
