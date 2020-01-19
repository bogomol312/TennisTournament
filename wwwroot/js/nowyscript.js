
    function PlayerSelectValid(){
      
      var select1 = document.getElementById("Player1Select");
      var select2 = document.getElementById("Player2Select");
      var textsel1 = select1.options[select1.selectedIndex].text;
      var textsel2 = select2.options[select2.selectedIndex].text;

      var labe = document.getElementById("PlayersValidation");

      if(textsel1==textsel2){
        labe.className="non-valid";
        select1.className="non-validinp";
        select2.className="non-validinp";
        return false;
      }
      else{
        labe.className="valid";
        select1.className="validinp";
        select2.className="validinp";
        return true;
      }

      return true;

    } 

    function RoleChecker() {
        var select1 = document.getElementById("Rola");
        var strUser = select1.options[select1.selectedIndex].value;
        var kraj = document.getElementById("Kraj");
        var plec = document.getElementById("Plec");
        var trener = document.getElementById("Trener");
        var element = document.getElementById("divzawodnikrole");

        if (strUser == "Zawodnik") {
            element.className = "zawodnikrole1";
        }
        else {
            element.className = "zawodnikrole"; 
        }
    }
    
    function ResultValidation(){
      var res1 = document.getElementById("Result1");
      var spanres1 = document.getElementById("Result1Validation");

      var res2 = document.getElementById("Result2");
      var spanres2 = document.getElementById("Result2Validation");
      
      var regex =/^\d{1,3}$/; //sprawdzenie na najawnosc
      
      if(!regex.test(res1.value)){
        spanres1.className="non-valid";//style.visibility = "visible";
        res1.className="non-validinp";//style.border = "2px red solid";
        
        if(!regex.test(res2.value)){
          spanres2.className="non-valid";//style.visibility = "visible";
          res2.className="non-validinp";//style.border = "2px red solid";
        }

        return false;
      }
      else{
        spanres1.className="valid";//style.visibility = "hidden";
        res1.className="validinp";//style.border = "0px white solid";
      }

      if(!regex.test(res2.value)){
        spanres2.className="non-valid";//style.visibility = "visible";
        res2.className="non-validinp";//style.border = "2px red solid";
        return false;
      }
      else{
        spanres2.className="valid";//style.visibility = "hidden";
        res2.className="validinp";//style.border = "0px white solid";
      }

      return true;
    }

    function ImieValidation(){

          var namestring = document.getElementById("Imie");
          var validnamelabel = document.getElementById("ImieValidation");

          var regex =/^([A-Z]|[a-z])+$/; //sprawdzenie na najawnosc
         
       //   console.log(regex.test(namestring.value));

        if(!regex.test(namestring.value)){ //sprawdzenie imie 
          validnamelabel.className="non-valid";//style.visibility = "visible";
          namestring.className="non-validinp";//style.border = "2px red solid";
          return false;
        }else{
          validnamelabel.className="valid";//style.visibility = "hidden";
          namestring.className="validinp";//style.border = "0px white solid";
        }
        return true;
      }

    function NazwiskoValidation(){
      var lastnamestring = document.getElementById("Nazwisko");
      var validlastnamelabel = document.getElementById("LastImieValidation");

      var regex =/^([A-Z]|[a-z])+$/; 

      if(!regex.test(lastnamestring.value)){//sprawdzenie nazwiska
        validlastnamelabel.className="non-valid";       //style.visibility = "visible";
        lastnamestring.className="non-validinp";        //style.border = "2px red solid";
        return false;
        }
        else{
          validlastnamelabel.className="valid";     //style.visibility = "hidden";
          lastnamestring.className="validinp";      //style.border = "0px white solid";
        }
        return true;
    }

    function SedziaValidation() {
        var select1 = document.getElementById("Sedzia");
        var strUser = select1.options[select1.selectedIndex].text;
        var labe = document.getElementById("SedziaValidation");

        if (strUser == "null") {
            labe.className = "non-valid";
            return false;
        }
        else {
            labe.className = "valid";
        }
        return true;
}

    function DateMeczValidation() {


        var inp = document.getElementById("DataUrodzenia");
        var text = document.getElementById("DataUrodzeniaValidation");

        var regex = /^\d{4}\-\d{1,2}\-\d{1,2}$/; //yyyy-mm-dd

        //console.log(regex.test(data.value));


        if (!regex.test(inp.value)) {
            inp.className = "non-validinp";//style.border = "2px red solid";
            text.className = "non-valid";//style.visibility = "visible";
            text.innerHTML = "Prosze poprawnie wprowadzic data";
            return false;
        }


        var parts = inp.value.split("/");
        var day = parseInt(parts[2], 10);
        var month = parseInt(parts[1], 10);
        var year = parseInt(parts[0], 10);


        if (year <= 2017 || year > 2020) {
            text.className = "non-valid";
            inp.className = "non-validinp";
            text.innerHTML = "Rok nie moze byc nizszy od 2018 oraz powyzej 2020";
            return false;
        }
        else if (month < 1 || month > 12) {
            text.className = "non-valid";
            text.innerHTML = "Prosze poprawnie wprowadzic miesiac";
            inp.className = "non-validinp";
            return false;
        }
        else if (day < 1 || day > 31) {
            text.className = "non-valid";
            text.innerHTML = "Prosze poprawnie wprowadzic dzien";
            inp.className = "non-validinp";
            return false;
        }
        else {
            text.className = "valid";
            inp.className = "validinp";
            return true;
        }

    }

// -------Validacja Daty-------
    function DateValidation(){

    var inp = document.getElementById("DataUrodzenia");
    var text = document.getElementById("DataUrodzeniaValidation");

        var regex = /^\d{4}\-\d{1,2}\-\d{1,2}$/; //yyyy-mm-dd

    //console.log(regex.test(data.value));
        

    if(!regex.test(inp.value)){
      inp.className="non-validinp";//style.border = "2px red solid";
      text.className="non-valid";//style.visibility = "visible";
      text.innerHTML = "Prosze poprawnie wprowadzic data";
      return false;
    }
    
  
    var parts = inp.value.split("/");
    var day = parseInt(parts[2], 10);
    var month = parseInt(parts[1], 10);
    var year = parseInt(parts[0], 10);

   // alert(day);
   //alert(month);
   //alert(year);
    

    if(year <= 1940 || year > 2001){
      text.className="non-valid";//style.visibility = "visible";
      inp.className="non-validinp";//style.border = "2px red solid";
      text.innerHTML = "Rok nie moze byc nizszy od 1940 oraz powyzej 2000";
      return false;
    }
    else if( month < 1 || month > 12){
      text.className="non-valid";//style.visibility = "visible";
      text.innerHTML = "Prosze poprawnie wprowadzic miesiac";
      inp.className="non-validinp";//style.border = "2px red solid";
      return false;
     }
      else if( day < 1 || day > 31 ){
      text.className="non-valid";//style.visibility = "visible";
      text.innerHTML = "Prosze poprawnie wprowadzic dzien";
      inp.className="non-validinp";//style.border = "2px red solid";
      return false;
    }
    else{
      text.className="valid";//style.visibility = "hidden";
      inp.className="validinp";//style.border = "0px white solid";
      return true;
    }

    return true;
 }

    function TrenerValidation() {

    }

    function ValidateFormMecz(event){

      var numbers = ResultValidation();
        console.log("numbers : "+numbers);

      var data = DateMeczValidation();
        console.log("dates : "+data);

      var sedzia = SedziaValidation();
        console.log("sedzia : " + sedzia);

      var plselect = PlayerSelectValid();
        console.log("players : "+plselect);

      if(numbers == false || data == false || plselect == false || sedzia == false)
         return false;

      return true;
    }

    function ValidateFormZaw(event){

      var name = ImieValidation();
      console.log("name : "+name);
      
      var lastname = NazwiskoValidation();
      console.log("lastname : "+lastname);

      var data = DateValidation();
        console.log("dates : " + data);

      if(!name || !data || !lastname)
        return false;

        return true;
    }

    