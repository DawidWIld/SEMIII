import random
import lorem

end_user = open("names.txt").read().splitlines()
category = ['Software', 'Hardware', 'Server', 'Network', 'Identity Management']

subcategory = [['Email', 'Browser', 'MS Office'],
['Laptop', 'Workstation', 'Printer'],
['Server1', 'Server2', 'Server3'],
['VPN', 'DNS', 'WLAN', 'LAN', 'Proxy'],
['Password Reset', 'Account Lockout']]

impact = ['Low', 'Medium', 'High']
urgency = ['1', '2', '3', '4']
assignment_group = ['Service Desk',  'Local Support', 'Network Team', 'Network Team', 'Service Desk']
affected_users = ['single user', '&lt;40%', '40% - 70%', '&gt;70%']
state = ['New', 'Work in progress', 'Awaiting info', 'Resolved']
company = ['DDMT00001', 'DDMT00002']

template = open("template.txt").read()

output = ''

for i in range (1,31):
	category_index = random.randint(0,4)
	subcategory_choice = random.choice(subcategory[category_index])

	output += template.format(
		f"INC0000{i:02}",
		random.choice(end_user),
		f"2018-10-{i:02}",
		random.choice(affected_users),
		random.choice(state),
		category[category_index],
		subcategory_choice,
		random.choice(impact),
		random.choice(urgency),
		assignment_group[category_index],
		"+48"+str(random.randint(500000000,600000000)),
		random.choice(company),
		f"I have an issue with my {subcategory_choice}",
		lorem.paragraph(),
		lorem.sentence())

open('workfile', 'w').write(output)
