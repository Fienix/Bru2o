function updateValues() {
    //Variables
    var grainCount = 8;

    //Get Grain Types and Defaults
    $.ajax({
        url: "/WaterProfiles/GetGrainTypes",
        type: "GET",
        dataType: "json",
        success: function (data) {
            var grainTypes = data;

            var test = grainTypes;

            //Create Grain Class
            function grain(id, type, weight, color, mash)
            {
                this.id = id;
                this.type = type;
                this.weight = weight;
                this.color = color;
                this.mash = mash;
            }

            //Create Grain Objects
            var grainObjects = new Array();
            for (i = 0; i < grainCount; i++) 
            { 
                grainObjects[i] =  new grain([i], document.getElementById("grainType" + [i]).value, document.getElementById("grainWeight" + [i]).value,
                                             document.getElementById("grainColor" + [i]).value, document.getElementById("grainMash" + [i]).value);
            }
    
            //Get all input values
            var startCalcium = document.getElementById("startCalcium").value;
            var startMagnesium = document.getElementById("startMagnesium").value;
            var startSodium = document.getElementById("startSodium").value;
            var startChloride = document.getElementById("startChloride").value;
            var startSulfate = document.getElementById("startSulfate").value;
            var startAlkalinity = document.getElementById("startAlkalinity").value;
            var mashGal = document.getElementById("mashGal").value;
            var spargeGal = document.getElementById("spargeGal").value;
            var mashDilution = document.getElementById("mashDilution").value;
            var spargeDilution = document.getElementById("spargeDilution").value;
            var gypsum = document.getElementById("gypsum").value;
            var calciumChloride = document.getElementById("calciumChloride").value;
            var epsomSalt = document.getElementById("epsomSalt").value;
            var acidMalt = document.getElementById("acidMalt").value;
            var lacticAcid = document.getElementById("lacticAcid").value;
            var slakedLime = document.getElementById("slakedLime").value;
            var bakingSoda = document.getElementById("bakingSoda").value;
            var chalk = document.getElementById("chalk").value;

            //Grain info
            var grainWeight = 0;
            for (i = 0; i < grainObjects.length; i++)
            {
                //if grain type isn"t selected, disable all other boxes.
                if (grainObjects[i].type == 1)
                {
                    document.getElementById("grainWeight" + [i]).value = 0;
                    document.getElementById("grainWeight" + [i]).disabled = true;
                    document.getElementById("grainColor" + [i]).disabled = true;
                    document.getElementById("grainMash" + [i]).disabled = true;
                    document.getElementById("grainWeight" + [i]).style.backgroundColor = "#9f9fa0";
                    document.getElementById("grainColor" + [i]).style.backgroundColor = "#9f9fa0";
                    document.getElementById("grainMash" + [i]).style.backgroundColor = "#9f9fa0";
                }
                else
                {
                    document.getElementById("grainWeight" + [i]).disabled = false;
                    document.getElementById("grainColor" + [i]).disabled = false;
                    document.getElementById("grainMash" + [i]).disabled = false;
                    document.getElementById("grainWeight" + [i]).style.backgroundColor = "";
                    document.getElementById("grainColor" + [i]).style.backgroundColor = "";
                    document.getElementById("grainMash" + [i]).style.backgroundColor = "";
                }

                //if grain isn"t crystal, disable color box
                if (grainObjects[i].type != 10) {
                    document.getElementById("grainColor" + [i]).disabled = true;
                    document.getElementById("grainColor" + [i]).style.backgroundColor = "#9f9fa0";
                } else {
                    document.getElementById("grainColor" + [i]).disabled = false;
                    document.getElementById("grainColor" + [i]).style.backgroundColor = "#fff";
                }

                //Add up grain weight
                grainWeight += parseFloat(grainObjects[i].weight);
            }

            //Do weight and thickness calculations
            var totalGrainWeight = parseFloat(grainWeight * 100 / 100).toFixed(2);
            document.getElementById("totalWeight").innerHTML = totalGrainWeight;
            document.getElementById("CalcStats_TotalGrainWeight").value = totalGrainWeight;
            document.getElementById("totalThickness").innerHTML = parseFloat((mashGal * 4 / totalGrainWeight) * 100 / 100 || 0).toFixed(2) + " qt/lb";
            document.getElementById("CalcStats_MashThickness").value = parseFloat((mashGal * 4 / totalGrainWeight) * 100 / 100 || 0).toFixed(2);

            //Sparge additions
            var spargeGypsumVal = gypsum / mashGal * spargeGal;
            spargeGypsumVal = Math.round(spargeGypsumVal * 100) / 100 || 0;
            document.getElementById("spargeGypsum").innerHTML = spargeGypsumVal;
            document.getElementById("CalcStats_SpargeGypsum").value = parseFloat(spargeGypsumVal).toFixed(2);
            var spargeCalciumChlorideVal = calciumChloride / mashGal * spargeGal || 0;
            spargeCalciumChlorideVal = Math.round(spargeCalciumChlorideVal * 100) / 100;
            document.getElementById("spargeCalciumChloride").innerHTML = spargeCalciumChlorideVal;
            document.getElementById("CalcStats_SpargeCalciumChloride").value = parseFloat(spargeCalciumChlorideVal).toFixed(2);
            var spargeEpsomSaltVal = epsomSalt / mashGal * spargeGal || 0;
            spargeEpsomSaltVal = Math.round(spargeEpsomSaltVal * 100) / 100;
            document.getElementById("spargeEpsomSalt").innerHTML = spargeEpsomSaltVal;
            document.getElementById("CalcStats_SpargeEpsomSalt").value = parseFloat(spargeEpsomSaltVal).toFixed(2);
            var spargeSlakedLimeVal = slakedLime / mashGal * spargeGal || 0;
            spargeSlakedLimeVal = Math.round(spargeSlakedLimeVal * 100) / 100;
            document.getElementById("spargeSlakedLime").innerHTML = spargeSlakedLimeVal;
            document.getElementById("CalcStats_SpargeSlakedLime").value = parseFloat(spargeSlakedLimeVal).toFixed(2);
            var spargeBakingSodaVal = bakingSoda / mashGal * spargeGal || 0;
            spargeBakingSodaVal = Math.round(spargeBakingSodaVal * 100) / 100;
            document.getElementById("spargeBakingSoda").innerHTML = spargeBakingSodaVal;
            document.getElementById("CalcStats_SpargeBakingSoda").value = parseFloat(spargeBakingSodaVal).toFixed(2);
            var spargeChalkVal = chalk / mashGal * spargeGal || 0;
            spargeChalkVal = Math.round(spargeChalkVal * 100) / 100;
            document.getElementById("spargeChalk").innerHTML = spargeChalkVal;
            document.getElementById("CalcStats_SpargeChalk").value = parseFloat(spargeChalkVal).toFixed(2);

            //Calculate Mash Water Profile
            var calcium = (1 - (mashDilution / 100)) * startCalcium + (chalk * 105.89 + gypsum * 60 + calciumChloride * 72 + slakedLime * 143) / mashGal || 0;
            document.getElementById("calcium").innerHTML = calcium.toFixed(0);
            document.getElementById("CalcStats_Calcium").value = calcium.toFixed(0);
            var magnesium = (1 - (mashDilution / 100)) * startMagnesium + epsomSalt * 24.6 / mashGal || 0;
            document.getElementById("magnesium").innerHTML = magnesium.toFixed(0);
            document.getElementById("CalcStats_Magnesium").value = magnesium.toFixed(0);
            var sodium = (1 - (mashDilution / 100)) * startSodium + bakingSoda * 72.3 / mashGal || 0;
            document.getElementById("sodium").innerHTML = sodium.toFixed(0);
            document.getElementById("CalcStats_Sodium").value = sodium.toFixed(0);
            var chloride = (1 - (mashDilution / 100)) * startChloride + calciumChloride * 127.47 / mashGal || 0;
            document.getElementById("chloride").innerHTML = chloride.toFixed(0);
            document.getElementById("CalcStats_Chloride").value = chloride.toFixed(0);
            var sulfate = (1 - (mashDilution / 100)) * startSulfate + (gypsum * 147.4 + epsomSalt * 103) / mashGal || 0;
            document.getElementById("sulfate").innerHTML = sulfate.toFixed(0);
            document.getElementById("CalcStats_Sulfate").value = sulfate.toFixed(0);
            var clsoRatio = chloride / sulfate || 0;
            document.getElementById("clsoRatio").innerHTML = clsoRatio.toFixed(2);
            document.getElementById("CalcStats_CSRatio").value = clsoRatio.toFixed(2);

            //TODO: Calculate Mash + Sparge Water Profile

            //Calculate Effective Alkalinity
            var lacticAcidContent = 0.88;
            var maltAcidContent= .02;
            var eAlkalinity = (1 - (mashDilution / 100)) * startAlkalinity * (50 / 61) + (chalk * 130 + bakingSoda * 157 - 176.1 * lacticAcid * lacticAcidContent * 2 - 4160.4 *
                maltAcidContent * acidMalt * 2.5 + slakedLime * 357) / mashGal || 0;
            document.getElementById("eAlkalinity").innerHTML = eAlkalinity.toFixed(0);
            document.getElementById("CalcStats_EffectiveAlk").value = eAlkalinity.toFixed(0);

            //Calculate Residual Alkalinity
            var rAlkalinity = eAlkalinity - ((calcium / 1.4) + magnesium / 1.7) || 0;
            document.getElementById("rAlkalinity").innerHTML = rAlkalinity.toFixed(0);
            document.getElementById("CalcStats_ResidualAlk").value = rAlkalinity.toFixed(0);

            //Calculate Grain pH
            if (!document.getElementById("manualPH").checked) {
                for (i = 0; i < grainObjects.length; i++) {
                    var mashPh = 0;
                    if (grainObjects[i].type == 10) {
                        mashPh = (5.22 - 0.00504 * parseFloat(grainObjects[i].color));
                    } else {
                        var grainType = $.grep(grainTypes, function(e) { return e.ID == grainObjects[i].type; });
                        mashPh = grainType[0].DefaultPH;
                    }

                    document.getElementById("grainMash" + [i]).value = mashPh.toFixed(2);
                    document.getElementById("grainMash" + [i]).disabled = true;
                }
            }

            //Calculate Mash pH
            var totalMashPh = 0;
            for (i = 0; i < grainObjects.length; i++) {
                var grainPh = document.getElementById("grainMash" + [i]).value;
                totalMashPh += grainObjects[i].weight * grainPh;
            }
            totalMashPh = totalMashPh / totalGrainWeight + (0.1085 * mashGal / totalGrainWeight + 0.013) * rAlkalinity / 50 || 0;
            document.getElementById("totalPH").innerHTML = totalMashPh.toFixed(2);
            document.getElementById("CalcStats_pH").value = totalMashPh.toFixed(2);
        }
    });
}