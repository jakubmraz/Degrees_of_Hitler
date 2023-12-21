def load_node_names(file_path):
    """
    Loads the node names from the given file.
    The file format is assumed to be: id name
    """
    node_names = {}
    with open(file_path, 'r') as file:
        for line in file:
            parts = line.strip().split(' ')
            node_id = int(parts[0])
            node_name = ' '.join(parts[1:])
            node_names[node_id] = node_name
    return node_names

def save_hitler_community_nodes(names_file, community_file, output_file):
    """
    Saves the node IDs and corresponding names for nodes in the Hitler community to an output file.
    """
    # Load the node names
    node_names = load_node_names(names_file)

    # Open the output file for writing
    with open(output_file, 'w') as output:
        # Process the community file
        with open(community_file, 'r') as file:
            for line in file:
                parts = line.strip().split(' ')
                node_id = int(parts[0])
                # Write the node ID and its corresponding name to the output file
                output.write(f'Node ID: {node_id}, Name: {node_names.get(node_id, "Unknown")}\n')

# Example usage
names_file = r'C:\Users\knedl\Desktop\wiki-topcats-page-names.txt'
community_file = 'community_thingy//module_18_output.txt'
output_file = 'hitler_community_nodes.txt'
save_hitler_community_nodes(names_file, community_file, output_file)
