-- setup
SetScriptTitle("Website Image Scraper")

-- functions
local function CheckTableForValue(t, v)
    for _, value in pairs(t) do
        if value == v then
            return true
        end
    end
    return false
end

-- main
local function main()
    -- start
    local targetUrl = Input("Enter the website url to scrape : ", "DarkYellow")

    -- data
    local urls = {
        jpeg = {},
        webp = {},
        png = {},
        svg = {}
    }

    -- start driver
    Print("\nStarting driver . . .", "DarkGray")
    local driver = StartDriver("chrome", 450, 600)
    local network = driver.Network
    
    -- hook network responses
    Print("Hooking network responses . . .", "DarkGray")
    local connection = network:Connect(function(data)
        -- check resource type
        if data.ResourceType ~= "Image" then return end

        -- check content type and add to table
        local contentType = data.Headers["content-type"]:gsub("image/", ""):lower()
        local urlFileExtension = data.Url:match("%.([^.]+)$"):lower()

        local urlTableCT = urls[contentType]
        local urlTableUFE = urls[urlFileExtension]
        if urlTableCT == nil and urlTableUFE == nil  then
            return
        end

        local urlTable = ((urlTableCT ~= nil) and urlTableCT or urlTableUFE)

        if not CheckTableForValue(urlTable, data.Url) then
            table.insert(urlTable, data.Url)
        end
    end, "responses")

    -- go to url
    Print("Opening website . . .", "DarkGray")
    driver:Browse(targetUrl)

    -- start scraping
    Input("Press enter to start scraping . . .", "DarkYellow")
    network:StartMonitoring()

    -- stop scraping
    Input("Press enter to stop scraping . . .", "DarkYellow")
    network:StopMonitoring()
    network:Disconnect(connection)
    driver:Quit()

    -- setup folders
    Print("Setting up folders . . .", "DarkGray")
    if DoesFolderExist("WebsiteImageScraper") then
        DeleteFolder("WebsiteImageScraper")
    end
    CreateFolder("WebsiteImageScraper")
    for urlKey, _ in pairs(urls) do
        CreateFolder("WebsiteImageScraper/"..urlKey)
    end

    -- download images
    Print("Downloading images . . .", "DarkGray")
    local counter = 0

    for urlKey, urlTable in pairs(urls) do
        for _, url in pairs(urlTable) do
            DownloadFile(
                url,
                "WebsiteImageScraper/"..urlKey.."/"..tostring(counter).."."..urlKey
            )
            counter = (counter + 1)
        end
        counter = 0
    end

    -- finish
    Print("Done!", "Green")
    Print("\nCheck the folder \"WebsiteImageScraper\" to see the downloaded images.", "DarkGray")
end

-- init
main()
