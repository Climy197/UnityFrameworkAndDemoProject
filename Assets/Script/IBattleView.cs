using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class uibase : MonoBehaviour,IBattleView
{
    public virtual void init()
    {
        var Battle=new Battle();
        Battle.bind(this);
        Battle.init();
    }
}

public interface IBattleView
{
    void hp(int hp);


}
public class  Battle {
    bind(IBattleView view);
    init(){

    }
    update(){

    }
    start(){

    }
}
public class BattleData {
    public int hp;
}