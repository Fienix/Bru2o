function updateValues() {
    //Variables
    var grainCount = 8;

    //Get Grain Types and Defaults
    $.ajax({
        url: '/WaterProfiles/GetGrainTypes',
        type: 'GET',
        dataType: 'json',
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
                grainObjects[i] =  new grain([i], document.getElementById('grainType' + [i]).value, document.getElementById('grainWeight' + [i]).value,
                                             document.getElementById('grainColor' + [i]).value, document.getElementById('grainMash' + [i]).value);
            }
    
            //Get all input values
            var startCalcium = document.getElementById('startCalcium').value;
            var startMagnesium = document.getElementById('startMagnesium').value;
            var startSodium = document.getElementById('startSodium').value;
            var startChloride = document.getElementById('startChloride').value;
            var startSulfate = document.getElementById('startSulfate').value;
            var startAlkalinity = document.getElementById('startAlkalinity').value;
            var mashGal = document.getElementById('mashGal').value;
            var spargeGal = document.getElementById('spargeGal').value;
            var mashDilution = document.getElementById('mashDilution').value;
            var spargeDilution = document.getElementById('spargeDilution').value;
            var gypsum = document.getElementById('gypsum').value;
            var calciumChloride = document.getElementById('calciumChloride').value;
            var epsomSalt = document.getElementById('epsomSalt').value;
            var acidMalt = document.getElementById('acidMalt').value;
            var lacticAcid = document.getElementById('lacticAcid').value;
            var slakedLime = document.getElementById('slakedLime').value;
            var bakingSoda = document.getElementById('bakingSoda').value;
            var chalk = document.getElementById('chalk').value;

            //Grain info
            var grainWeight = 0;
            for (i = 0; i < grainObjects.length; i++)
            {
                //if grain type isn't selected, disable all other boxes.
                if (grainObjects[i].type == 1)
                {
                    document.getElementById('grainWeight' + [i]).disabled = true;
                    document.getElementById('grainColor' + [i]).disabled = true;
                    document.getElementById('grainMash' + [i]).disabled = true;
                }
                else
                {
                    document.getElementById('grainWeight' + [i]).disabled = false;
                    document.getElementById('grainColor' + [i]).disabled = false;
                    document.getElementById('grainMash' + [i]).disabled = false;
                }

                //if grain isn't crystal, disable color box
                if (grainObjects[i].type != 10) { document.getElementById('grainColor' + [i]).disabled = true; }
                else { document.getElementById('grainColor' + [i]).disabled = false; }

                //Add up grain weight
                grainWeight += parseFloat(grainObjects[i].weight);
            }

            //Calculate Grain pH
            if (!document.getElementById('manualPH').checked)
            {
                for (i = 0; i < grainObjects.length; i++)
                {
                    var mashPH = 0;
                    if (grainObjects[i].type == 10) {
                        mashPH = (5.22 - 0.00504 * parseFloat(grainObjects[i].color));
                    }
                    else
                    {
                        var grainType = $.grep(grainTypes, function (e) { return e.ID == grainObjects[i].type; });
                        mashPH = grainType[0].DefaultPH;
                    }

                    document.getElementById('grainMash' + [i]).value = mashPH.toFixed(2);
                }
            }

            //Do weight and thickness calculations
            var totalGrainWeight = parseFloat(grainWeight * 100 / 100).toFixed(2);
            document.getElementById('totalWeight').innerHTML = totalGrainWeight;
            document.getElementById('totalThickness').innerHTML = parseFloat((mashGal * 4 / totalGrainWeight) * 100 / 100).toFixed(2) + ' qt/lb';

            //Sparge additions
            var spargeGypsumVal = gypsum / mashGal * spargeGal;
            spargeGypsumVal = Math.round(spargeGypsumVal * 100) / 100
            document.getElementById('spargeGypsum').innerHTML = spargeGypsumVal;
            var spargeCalciumChlorideVal = calciumChloride / mashGal * spargeGal;
            spargeCalciumChlorideVal = Math.round(spargeCalciumChlorideVal * 100) / 100
            document.getElementById('spargeCalciumChloride').innerHTML = spargeCalciumChlorideVal;
            var spargeEpsomSaltVal = epsomSalt / mashGal * spargeGal;
            spargeEpsomSaltVal = Math.round(spargeEpsomSaltVal * 100) / 100
            document.getElementById('spargeEpsomSalt').innerHTML = spargeEpsomSaltVal;
            var spargeSlakedLimeVal = slakedLime / mashGal * spargeGal;
            spargeSlakedLimeVal = Math.round(spargeSlakedLimeVal * 100) / 100
            document.getElementById('spargeSlakedLime').innerHTML = spargeSlakedLimeVal;
            var spargeBakingSodaVal = bakingSoda / mashGal * spargeGal;
            spargeBakingSodaVal = Math.round(spargeBakingSodaVal * 100) / 100
            document.getElementById('spargeBakingSoda').innerHTML = spargeBakingSodaVal;
            var spargeChalkVal = chalk / mashGal * spargeGal;
            spargeChalkVal = Math.round(spargeChalkVal * 100) / 100
            document.getElementById('spargeChalk').innerHTML = spargeChalkVal;

            //Set new values
        }
    });
}