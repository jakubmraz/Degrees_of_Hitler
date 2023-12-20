def filter_module_18(input_file, output_file):
    with open(input_file, 'r') as infile, open(output_file, 'w') as outfile:
        for line in infile:
            # Skip lines starting with '#'
            if line.startswith('#'):
                continue

            # Splitting the line into components
            parts = line.split()

            # Check if the line has enough parts and if the module part equals '18'
            if len(parts) > 1 and parts[1] == '18':
                outfile.write(line)

# Replace 'wiki-topcats.txt' with the path to your input file
input_file_path = 'community_thingy//wiki-topcats.clu'

# The output file will contain only lines where module=18
output_file_path = 'community_thingy//module_18_output.txt'

# Call the function
filter_module_18(input_file_path, output_file_path)

print("Filtering complete. Check module_18_output.txt for results.")