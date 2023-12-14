from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
import pytest
options = webdriver.ChromeOptions()
options.add_argument("start-maximized") 
options.add_argument("disable-infobars") 
options.add_argument("--disable-extensions") 
options.add_argument("--disable-dev-shm-usage") 
options.add_argument("--no-sandbox") 
options.add_argument("--headless") 
driver = webdriver.Chrome(options)
driver.get("http://localhost:5276/Book")

def test_if_containts_one_book_generated_by_seeder():
    table = driver.find_element(By.CLASS_NAME,"table")
    headers = table.find_elements(By.TAG_NAME, "th")
    assert len(headers) == 3, f"expected 3 columns in table got {len(headers)}"
    headers = table.find_elements(By.TAG_NAME,"tr")
    assert len(headers) > 1, f"data from seeder not generating"

def test_if_adds_book_correctly():
    link = driver.find_element(By.TAG_NAME, "p")
    button = link.find_element(By.TAG_NAME, "a")
    button.click()
    driver.find_element(By.ID, "Id").send_keys(5)
    driver.find_element(By.ID, "Title").send_keys("TI")
    driver.find_element(By.ID, "Author").send_keys("JOHN")
    driver.find_element(By.XPATH, "//input[@type='submit' and @value='Create']").click()
    table = driver.find_element(By.CLASS_NAME,"table")
    headers = table.find_elements(By.TAG_NAME,"tr")
    title = headers[2].text.split( )[0]
    author = headers[2].text.split( )[1]
    assert title in "TI", "not working adding book, incorrect title"
    assert author in "JOHN", "not working adding book, incorrect author"
def test_if_edit_book_correctly():
    driver.find_element(By.XPATH, "//a[@href='/Book/Edit/5']").click();
    driver.find_element(By.ID, "Title").send_keys("IT")
    driver.find_element(By.ID, "Author").send_keys("NY")
    driver.find_element(By.XPATH, "//input[@type='submit' and @value='Save']").click()
    table = driver.find_element(By.CLASS_NAME,"table")
    headers = table.find_elements(By.TAG_NAME,"tr")
    title = headers[2].text.split( )[0]
    author = headers[2].text.split( )[1]
    assert title in "IT", "not working editing book, incorrect title"
    assert author in "JOHNNY", "not working editing book, incorrect author"

def test_if_delete_book_works():
    driver.find_element(By.XPATH, "//a[@href='/Book/Delete/5']").click();
    driver.find_element(By.XPATH, "//input[@type='submit' and @value='Delete']").click()
    table = driver.find_element(By.CLASS_NAME,"table")
    headers = table.find_elements(By.TAG_NAME,"tr")
    assert len(headers) == 2, "deletion doesn't delete element"


