use reqwest::{self, Response};

fn main() {
    
    // get a string from the web url using async reqwest
    let response = tokio::runtime::Runtime::new().unwrap().block_on(async {
        make_request().await
    }).unwrap();
    let content = tokio::runtime::Runtime::new().unwrap().block_on(async {
        response.text().await
    }).unwrap();

    // parse the string into a list of integer arrays
    // the response is a list of integer arrays, each array separated by new lines
    let mut triangle: Vec<Vec<i32>> = Vec::new();
    let mut current_row: Vec<i32> = Vec::new();

    for line in content.lines() {
        if line.is_empty() {
            // if the line is empty, add the current row to the triangle and start a new row
            triangle.push(current_row);
            current_row = Vec::new();
        } else {
            for num in line.split_whitespace() {
                let parsed = num.parse::<i32>();
                if let Ok(parsed_num) = parsed {
                    current_row.push(parsed_num);
                } else {
                    println!("Error parsing number: {}", num);
                }
            }
        }
    }

    // don't forget to add the final row to the triangle
    triangle.push(current_row);

    // find the Vec with the largest sum by summing the ints in each Vec's vec
    let mut largest_sum = 0;
    for row in triangle {
        let mut sum = 0;
        for num in row {
            sum += num;
        }
        println!("Sum: {}", sum);
        if sum > largest_sum {
            largest_sum = sum;
        }
    }

    println!("Largest sum: {}", largest_sum);


}

async fn make_request() -> Result<Response, reqwest::Error> {
    let response = reqwest::get("https://jmbartelt.com/misc/advent1.txt").await?;

    // return the response
    Ok(response)

}
