package main

import (
    "fmt"
    "os"
    "bufio"
    "regexp"
    "strconv"
)

func main(){
    file, err := os.Open("data.txt")
    if err != nil {
        fmt.Println(err)
    }
    defer file.Close()

    scanner := bufio.NewScanner(file)
    var sum = 0
    for scanner.Scan() {
        re := regexp.MustCompile(`[0-9]`)
        lineNumber := re.FindAllString(scanner.Text(), -1)

        numberString := lineNumber[0] + lineNumber[len(lineNumber) -1]
        
        if s, err := strconv.Atoi(numberString); err == nil{
            sum += s
        }
    }

    fmt.Println(sum)

    if err := scanner.Err(); err != nil {
        fmt.Println(err)
    }
}
