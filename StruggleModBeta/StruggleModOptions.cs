using Menu.Remix.MixedUI;
using Menu.Remix.MixedUI.ValueTypes;
using UnityEngine;

namespace StruggleMod;

public class StruggleModOptions : OptionInterface
{  
    //Initialize instance
    public static StruggleModOptions instance;
    
    //initialize slider and config for struggle value
    private OpSlider struggle_slider;

    public static Configurable<int> struggle_configurable;

    public StruggleModOptions()
    {
        instance = this;
        struggle_configurable = config.Bind("struggle_slider", 25, new ConfigurableInfo("", null, ""));
    }

    public override void Initialize()
    {
        Debug.Log("Struggle Options menu initialize");
        base.Initialize();
        Tabs = new OpTab[1]
        {
            new OpTab(this, "Overview")
        };

        OpLabel struggle_label = new OpLabel(new Vector2(300f, 500f), new Vector2(1f, 1f), "Inputs until free", FLabelAlignment.Center, true);

        struggle_slider = new OpSlider(struggle_configurable, new Vector2(250f, 470f), 100){
            description = "Number of inputs before you break free."
        };

        struggle_slider.min = 0;
        struggle_slider.max = 100;

        Tabs[0].AddItems(struggle_label, struggle_slider);
    }

    public override void Update()
    {
        //Directly call the base Update() function, since there where no modifications to it
        base.Update();
    }

}