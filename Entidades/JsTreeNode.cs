﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class JsTreeNode
    {            
        public string id { get; set; }      // will be autogenerated if omitted
        public string text { get; set; }    // node text
        public string icon { get; set; }    // string for custom
        public State state { get; set; }
        public List<JsTreeNode> children { get; set; }  // array of strings or objects
        //li_attr     : {}  // attributes for the generated LI node
        //a_attr      : {}  // attributes for the generated A node

        public string idPropio { get; set; }
        public bool EsPrograma { get; set; }
        public bool EsCurso { get; set; }


        public JsTreeNode()
        {
            children = new List<JsTreeNode>();
            state = new State();
        }
    }

    public class State
    {
        public bool opened { get; set; }    // is the node open
        public bool disabled { get; set; }  // is the node disabled
        public bool selected { get; set; }  // is the node selected
    }
}