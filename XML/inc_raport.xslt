<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml"
                              xmlns:s="http://priorityMatrix.data">
  
  <xsl:output method="xml" indent="yes"/>

  <xsl:key name="companyById" match="company" use="@id"/>
  <xsl:key name="priorityLookup" match="s:matcher" use="s:impact"/>
  
  <xsl:variable name="matcher-top" select="document('')/*/s.matcher"/>

  <xsl:template match="/root">
    <root>
      <xsl:apply-templates select="incidents"/>
      <stats>
        <numberOfIncidents>
          <xsl:value-of select="count(/root/incidents/*)"/>
        </numberOfIncidents>
        <!--<reportDate>
          <xsl:value-of  select="current-dateTime()"/>
        </reportDate>-->
        <mostCategory>
        </mostCategory>
      </stats>
    </root>
  </xsl:template>

  <xsl:template match="incidents">
    <incidents>
      <xsl:apply-templates select="incident">
        <xsl:sort select="state"/>
      </xsl:apply-templates>
    </incidents>
  </xsl:template>
  
  <xsl:template match="incident">
    <incident>
      <incidentID>
        <xsl:value-of select="incidentID" />
      </incidentID>
      <endUser>
        <xsl:value-of select="endUser" />
      </endUser>
      <dateOpened>
        <xsl:value-of select="dateOpened" />
      </dateOpened>
      <affectedUsers>
        <xsl:value-of select="affectedUsers" />
      </affectedUsers>
      <state>
        <xsl:value-of select="state" />
      </state>
      <category>
        <mainCategory>
          <xsl:value-of select="category/mainCategory" />
        </mainCategory>
        <subcategory>
          <xsl:value-of select="category/subcategory" />
        </subcategory>
      </category>
      <priority>
      <xsl:apply-templates select="$matcher-top">
        <xsl:with-param name="curr-incident" select="."/>
      </xsl:apply-templates>
        </priority>
      <!--<priority>
        --><!--<impact>
          <xsl:value-of select="priority/impact" />
        </impact>
        <urgency>
          <xsl:value-of select="priority/urgency" />
        </urgency>--><!--
      </priority>-->
      
      <assignmentGroup>
        <xsl:value-of select="assignmentGroup" />
      </assignmentGroup>
      <phoneNumber>
        <xsl:value-of select="phoneNumber" />
      </phoneNumber>
      <userCompany>
        <xsl:value-of select="key('companyById', userCompany/@comid)" />
      </userCompany>
      <shortDescription>
        <xsl:value-of select="shortDescription" />
      </shortDescription>
      <description>
        <xsl:value-of select="description" />
      </description>
      <workNotes>
        <xsl:value-of select="workNotes" />
      </workNotes>
    </incident>
  </xsl:template>
  
 <!--<xsl:template match="convert">
    <xsl:choose>
      <xsl:when test="priority/impact = Low and priority/urgency = 1 "
    </xsl:choose>
  
  </xsl:template>-->
    
  <xsl:template match="s:matcher">
    <xsl:param name="curr-incident"/>
    <xsl:value-of select="key('prioritylookup', $curr-incident/priority/impact)/s:priority" />
    <!--<xsl:value-of select="key('priorityLookup', concat($curr-incident/priority/impact, '|', $curr-incident/priority/urgency))/s:priority" />-->
  </xsl:template>
  
  <s:matcher>
    <s:match><s:impact>Low</s:impact><s:urgency>1</s:urgency><s:priority>3</s:priority></s:match>
    <s:match><s:impact>Low</s:impact><s:urgency>2</s:urgency><s:priority>3</s:priority></s:match>
    <s:match><s:impact>Low</s:impact><s:urgency>3</s:urgency><s:priority>4</s:priority></s:match>
    <s:match><s:impact>Low</s:impact><s:urgency>4</s:urgency><s:priority>4</s:priority></s:match>
 
    <s:match><s:impact>Medium</s:impact><s:urgency>1</s:urgency><s:priority>1</s:priority></s:match>
    <s:match><s:impact>Medium</s:impact><s:urgency>2</s:urgency><s:priority>2</s:priority></s:match>
    <s:match><s:impact>Medium</s:impact><s:urgency>3</s:urgency><s:priority>3</s:priority></s:match>
    <s:match><s:impact>Medium</s:impact><s:urgency>4</s:urgency><s:priority>4</s:priority></s:match>

    <s:match><s:impact>High</s:impact><s:urgency>1</s:urgency><s:priority>1</s:priority></s:match>
    <s:match><s:impact>High</s:impact><s:urgency>2</s:urgency><s:priority>1</s:priority></s:match>
    <s:match><s:impact>High</s:impact><s:urgency>3</s:urgency><s:priority>2</s:priority></s:match>
    <s:match><s:impact>High</s:impact><s:urgency>4</s:urgency><s:priority>3</s:priority></s:match>
  </s:matcher>

</xsl:stylesheet>
