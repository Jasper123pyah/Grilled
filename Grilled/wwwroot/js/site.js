// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#type").change(function () {
        var val = $(this).val();
        if (val == "Top")
        {
            $("#size").html("<option value='US XXS / EU 40'>US XXS / EU 40</option> <option value='US XS / EU 42 / 0'>US XS / EU 42 / 0</option> <option value='US S / EU 44-46 / 1'>US S / EU 44-46 / 1</option> <option value='US M / EU 48-50 / 2'>US M / EU 48-50 / 2</option> <option value='US L / EU 52-54 / 3'>US L / EU 52-54 / 3</option> <option value='US XL / EU 56 / 4'>US XL / EU 56 / 4</option> <option value='US XXL / EU 58 / 5'>US XXL / EU 58 / 5</option>");
        }
        else if (val == "Bottom")
        {
            $("#size").html("<option value='US 26 / EU 42'>US 26 / EU 42</option><option value='US 27 / EU 43'>US 27 / EU 43</option><option value='US 28 / EU 44'>US 28 / EU 44</option><option value='US 29 / EU 45'>US 29 / EU 45</option><option value='US 30 / EU 46'>US 30 / EU 46</option><option value='US 31 / EU 47'>US 31 / EU 47</option><option value='US 32 / EU 48'>US 32 / EU 48</option><option value='US 33 / EU 49'>US 33 / EU 49</option><option value='US 34 / EU 50'>US 34 / EU 50</option><option value='US 35 / EU 51'>US 35 / EU 51</option><option value='US 36 / EU 52'>US 36 / EU 52</option><option value='US 37 / EU 53'>US 37 / EU 53</option><option value='US 38 / EU 54'>US 38 / EU 54</option><option value='US 39 / EU 55'>US 39 / EU 55</option><option value='US 40 / EU 56'>US 40 / EU 56</option><option value='US 41 / EU 57'>US 41 / EU 57</option><option value='US 42 / EU 58'>US 42 / EU 58</option><option value='US 43 / EU 59'>US 43 / EU 59</option><option value='US 44 / EU 60'>US 44 / EU 60</option>");
        }
        else if (val == "Footwear")
        {
            $("#size").html("<option value='US 5 / EU 37'>US 5 / EU 37</option><option value='US 5.5 / EU 38'>US 5.5 / EU 38</option><option value='US 6 / EU 39'>US 6 / EU 39</option><option value='US 6.5 / EU 39-40'>US 6.5 / EU 39-40</option><option value='US 7 / EU 40'>US 7 / EU 40</option><option value='US 7.5 / EU 40-41'>US 7.5 / EU 40-41</option><option value='US 8 / EU 41'>US 8 / EU 41</option><option value='US 8.5 / EU 41-42'>US 8.5 / EU 41-42</option><option value='US 9 / EU 42'>US 9 / EU 42</option><option value='US 9.5 / EU 42-43'>US 9.5 / EU 42-43</option><option value='US 10 / EU 43'>US 10 / EU 43</option><option value='US 10.5 / EU 43-44'>US 10.5 / EU 43-44</option><option value='US 11 / EU 44'>US 11 / EU 44</option><option value='US 11.5 / EU 44-45'>US 11.5 / EU 44-45</option><option value='US 12 / EU 45'>US 12 / EU 45</option><option value='US 12.5 / EU 45-46'>US 12.5 / EU 45-46</option><option value='US 13 / EU 46'>US 13 / EU 46</option><option value='US 14 / EU 47'>US 14 / EU 47</option><option value='US 15 / EU 48'>US 15 / EU 48</option>");
        }
        else if (val == "Outerwear")
        {
            $("#size").html("<option value='US XXS / EU 40'>US XXS / EU 40</option> <option value='US XS / EU 42 / 0'>US XS / EU 42 / 0</option> <option value='US S / EU 44-46 / 1'>US S / EU 44-46 / 1</option> <option value='US M / EU 48-50 / 2'>US M / EU 48-50 / 2</option> <option value='US L / EU 52-54 / 3'>US L / EU 52-54 / 3</option> <option value='US XL / EU 56 / 4'>US XL / EU 56 / 4</option> <option value='US XXL / EU 58 / 5'>US XXL / EU 58 / 5</option>");          
        }
        else if (val == "Accessoires")
        {
            $("#size").html("<option value='OS'>One Size</option>");
        }
    });
});