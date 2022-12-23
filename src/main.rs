use std::io::Read;
use reqwest::{self, Response};

fn main() {
    
    // get a string from the input_1.txt stored locally
    let mut content = String::new();
    let mut file = std::fs::File::open("input_1.txt").unwrap();
    file.read_to_string(&mut content).unwrap();    

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
    let mut largest_sum = i32::min_value();
    let mut second_largest_sum = i32::min_value();
    let mut third_largest_sum = i32::min_value();
    for row in triangle {
        let sum = row.iter().sum::<i32>();
        if sum > largest_sum {
            third_largest_sum = second_largest_sum;
            second_largest_sum = largest_sum;
            largest_sum = sum;
        } else if sum > second_largest_sum {
            third_largest_sum = second_largest_sum;
            second_largest_sum = sum;
        } else if sum > third_largest_sum {
            third_largest_sum = sum;
        }
    }

    println!("Third largest sum: {}", third_largest_sum);
    println!("Second largest sum: {}", second_largest_sum);
    println!("Largest sum: {}", largest_sum);
    println!("Top three total: {}", third_largest_sum + second_largest_sum + largest_sum);


}
