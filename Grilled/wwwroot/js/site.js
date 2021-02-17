// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#type").change(function () {
        var val = $(this).val();
        if (val == "Top")
        {
            $("#size").html("<option value='XXS'>US XXS / EU 40</option> <option value='XS'>US XS / EU 42 / 0</option> <option value='S'>US S / EU 44-46 / 1</option> <option value='M'>US M / EU 48-50 / 2</option> <option value='L'>US L / EU 52-54 / 3</option> <option value='XL'>US XL / EU 56 / 4</option> <option value='XXL'>US XXL / EU 58 / 5</option>");
        }
        else if (val == "Bottom")
        {
            $("#size").html("<option value='26'>US 26 / EU 42</option><option value='27'>US 27 / EU 43</option><option value='28'>US 28 / EU 44</option><option value='29'>US 29 / EU 45</option><option value='30'>US 30 / EU 46</option><option value='31'>US 31 / EU 47</option><option value='32'>US 32 / EU 48</option><option value='33'>US 33 / EU 49</option><option value='34'>US 34 / EU 50</option><option value='35'>US 35 / EU 51</option><option value='36'>US 36 / EU 52</option><option value='37'>US 37 / EU 53</option><option value='38'>US 38 / EU 54</option><option value='39'>US 39 / EU 55</option><option value='40'>US 40 / EU 56</option><option value='41'>US 41 / EU 57</option><option value='42'>US 42 / EU 58</option><option value='43'>US 43 / EU 59</option><option value='44'>US 44 / EU 60</option>");
        }
        else if (val == "Footwear")
        {
            $("#size").html("<option value='5'>US 5 / EU 37</option><option value='5.5'>US 5.5 / EU 38</option><option value='6'>US 6 / EU 39</option><option value='6.5'>US 6.5 / EU 39-40</option><option value='7'>US 7 / EU 40</option><option value='7.5'>US 7.5 / EU 40-41</option><option value='8'>US 8 / EU 41</option><option value='8.5'>US 8.5 / EU 41-42</option><option value='9'>US 9 / EU 42</option><option value='9.5'>US 9.5 / EU 42-43</option><option value='10'>US 10 / EU 43</option><option value='10.5'>US 10.5 / EU 43-44</option><option value='11'>US 11 / EU 44</option><option value='11.5'>US 11.5 / EU 44-45</option><option value='12'>US 12 / EU 45</option><option value='12.5'>US 12.5 / EU 45-46</option><option value='13'>US 13 / EU 46</option><option value='14'>US 14 / EU 47</option><option value='15'>US 15 / EU 48</option>");
        }
        else if (val == "Outerwear")
        {
            $("#size").html("<option value='XXS'>US XXS / EU 40</option> <option value='XS'>US XS / EU 42 / 0</option> <option value='S'>US S / EU 44-46 / 1</option> <option value='M'>US M / EU 48-50 / 2</option> <option value='L'>US L / EU 52-54 / 3</option> <option value='XL'>US XL / EU 56 / 4</option> <option value='XXL'>US XXL / EU 58 / 5</option>");          
        }
        else if (val == "Accessoires")
        {
            $("#size").html("<option value='OS'>One Size</option>");
        }
    });
});